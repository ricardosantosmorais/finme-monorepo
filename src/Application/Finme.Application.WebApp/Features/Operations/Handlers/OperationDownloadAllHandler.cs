using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using Finme.Application.WebApp.DTOs;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.IO.Compression;
using Finme.Application.WebApp.Features.Operations.Commands;

namespace Finme.Application.WebApp.Features.Operations.Handlers
{
    public class OperationDownloadAllCommandHandler(
        IRepository<Operation> operationRepository,
        IRepository<OperationFile> operationDocumentsRepository,
        IAmazonS3 s3Client,
        IConfiguration configuration,
        IMapper mapper) : IRequestHandler<OperationDownloadAllCommand, DownloadDocumentResponse>
    {
        private readonly IRepository<Operation> _operationRepository = operationRepository;
        private readonly IRepository<OperationFile> _operationDocumentsRepository = operationDocumentsRepository;
        private readonly IAmazonS3 _s3Client = s3Client;
        private readonly string _bucketName = configuration["AWS:S3:BucketName"];
        private readonly IMapper _mapper = mapper;

        public async Task<DownloadDocumentResponse> Handle(OperationDownloadAllCommand request, CancellationToken cancellationToken)
        {
            // Validar operação
            var operation = await _operationRepository.FindAsync(u => u.Id == request.OperationId);
            if (operation == null)
                throw new Exception("Operação não foi encontrada");

            if (string.IsNullOrEmpty(_bucketName))
                throw new Exception("Nome do bucket S3 não configurado.");

            // Buscar todos os documentos da operação
            var documents = await _operationDocumentsRepository.Get(x => x.OperationId == request.OperationId);
            if (!documents.Any())
                throw new Exception("Nenhum documento encontrado para esta operação");

            // Criar um MemoryStream para o arquivo ZIP
            var zipStream = new MemoryStream();
            try
            {
                using (var zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Create, leaveOpen: true))
                {
                    foreach (var document in documents)
                    {
                        // Baixar o arquivo do S3
                        var s3Request = new GetObjectRequest
                        {
                            BucketName = _bucketName,
                            Key = document.Key
                        };

                        using var s3Response = await _s3Client.GetObjectAsync(s3Request, cancellationToken);
                        using var fileStream = new MemoryStream();
                        await s3Response.ResponseStream.CopyToAsync(fileStream, cancellationToken);
                        fileStream.Position = 0;

                        // Adicionar o arquivo ao ZIP
                        var fileName = document.Name.Split('/').Last();
                        var zipEntry = zipArchive.CreateEntry(fileName);
                        using var entryStream = zipEntry.Open();
                        await fileStream.CopyToAsync(entryStream, cancellationToken);
                    }
                }

                // Resetar a posição do stream do ZIP
                zipStream.Position = 0;

                // Preparar a resposta
                return new DownloadDocumentResponse
                {
                    FileStream = zipStream,
                    FileName = $"{operation.Name}_Documents.zip",
                    ContentType = "application/zip"
                };
            }
            catch
            {
                // Garantir que o stream seja descartado em caso de erro
                zipStream.Dispose();
                throw;
            }
        }
    }
}