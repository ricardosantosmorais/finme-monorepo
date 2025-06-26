using Amazon.S3.Model;
using Amazon.S3;
using AutoMapper;
using Finme.Application.WebApp.DTOs;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Finme.Application.WebApp.Features.Operations.Commands;

namespace Finme.Application.WebApp.Features.Operations.Handlers
{
    public class OperationDownloadCommandHandler(
        IRepository<Operation> operationRepository,
        IRepository<OperationFile> operationDocumentsRepository,
        IAmazonS3 s3Client,
        IConfiguration configuration,
        IMapper mapper) : IRequestHandler<OperationDownloadCommand, DownloadDocumentResponse>
    {
        private readonly IRepository<Operation> _operationRepository = operationRepository;
        private readonly IRepository<OperationFile> _operationDocumentsRepository = operationDocumentsRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IAmazonS3 _s3Client = s3Client;
        private readonly string _bucketName = configuration["AWS:S3:BucketNameOperation"];

        public async Task<DownloadDocumentResponse> Handle(OperationDownloadCommand request, CancellationToken cancellationToken)
        {
            var operation = await _operationRepository.FindAsync(u => u.Id == request.OperationId);
            if (operation == null)
                throw new Exception("Operação não foi encontrada");

            if (string.IsNullOrEmpty(_bucketName))
            {
                throw new Exception("Nome do bucket S3 não configurado.");
            }

            var document = await _operationDocumentsRepository.FindAsync(x => x.OperationId == request.OperationId && x.Id == request.DocumentId);
            if (document == null)
                throw new Exception("Documento não encontrado");

            var s3Request = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = document.Key
            };

            using var response = await _s3Client.GetObjectAsync(s3Request, cancellationToken);

            // Copiar o ResponseStream para um MemoryStream
            var memoryStream = new MemoryStream();
            await response.ResponseStream.CopyToAsync(memoryStream, cancellationToken);
            memoryStream.Position = 0; // Resetar a posição do stream

            var contentType = response.Headers.ContentType ?? GetContentTypeFromExtension(document.Name);
            var fileName = document.Name.Split('/').Last();

            return new DownloadDocumentResponse
            {
                FileStream = memoryStream,
                FileName = fileName,
                ContentType = contentType
            };
        }

        // Função auxiliar para determinar ContentType com base na extensão
        private string GetContentTypeFromExtension(string key)
        {
            var extension = Path.GetExtension(key)?.ToLowerInvariant();
            return extension switch
            {
                ".pdf" => "application/pdf",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                _ => "application/octet-stream"
            };
        }
    }
}
