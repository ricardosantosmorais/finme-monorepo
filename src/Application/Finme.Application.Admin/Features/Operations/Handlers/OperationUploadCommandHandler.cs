using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Operations.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Finme.Application.Admin.Features.Operations.Handlers
{
    public class OperationUploadCommandHandler(
        IRepository<Operation> operationRepository,
        IRepository<OperationFile> operationFileRepository,
        IAmazonS3 s3Client,
        IConfiguration configuration,
        IMapper mapper) : IRequestHandler<OperationUploadCommand, ResultDto<int>>
    {
        private readonly IRepository<Operation> _operationRepository = operationRepository;
        private readonly IRepository<OperationFile> _operationFileRepository = operationFileRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IAmazonS3 _s3Client = s3Client;
        private readonly string _bucketName = configuration["AWS:S3:BucketNameOperation"];

        public async Task<ResultDto<int>> Handle(OperationUploadCommand request, CancellationToken cancellationToken)
        {
            var operation = await _operationRepository.GetByIdAsync(request.OperationId);
            if (operation == null)
                throw new Exception("Operação não foi encontrada");

            if (string.IsNullOrEmpty(_bucketName))
            {
                throw new Exception("Nome do bucket S3 não configurado.");
            }

            var file = request.File;

            if (file.Length == 0)
            {
                throw new Exception($"Arquivo {file.FileName} está vazio.");
            }

            // Gerar um nome único para o arquivo
            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var key = $"documents/{request.OperationId}/{uniqueFileName}"; // Organiza por userId

            // Configurar a requisição de upload para o S3
            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = key,
                InputStream = file.OpenReadStream(),
                ContentType = request.ContentType
            };

            // Fazer o upload do arquivo para o S3
            var response = await _s3Client.PutObjectAsync(putRequest, cancellationToken);

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Falha ao enviar o arquivo {file.FileName} para o S3.");
            }

            var newFile = new OperationFile
            {
                Type = request.ContentType,
                OperationId = request.OperationId,
                Size = file.Length.ToString(),
                Name = file.FileName,
                Key = key,
                DocumentType = request.DocumentType,
                BucketName = _bucketName,
                CreatedAt = DateTime.UtcNow,
                Active = true
            };

            await _operationFileRepository.AddAsync(newFile);
            await _operationFileRepository.SaveChangesAsync();

            if (!String.IsNullOrEmpty(request.FieldName)) {

                if(request.FieldName == "ContractFile")
                {
                    if (!String.IsNullOrEmpty(operation.ContractFile))
                    {
                        var contractFile = await _operationFileRepository.FindAsync(x => x.Id == Convert.ToInt32(operation.ContractFile.ToString()));
                        if (contractFile != null)
                        {
                            contractFile.DeletedAt = DateTime.UtcNow;
                            contractFile.Active = false;
                            _operationFileRepository.Update(contractFile);
                            await _operationFileRepository.SaveChangesAsync();
                        }

                    }
                }

                Type type = operation.GetType();
                PropertyInfo? propertyInfo = type.GetProperty(request.FieldName);
                if (propertyInfo != null && propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(operation, newFile.Id.ToString());
                }

                _operationRepository.Update(operation);
                await _operationRepository.SaveChangesAsync();

            }

            // Retornar o DTO com os dados do usuário e o token
            return ResultDto<int>.SuccessResult(newFile.Id, "Arquivos enviados com sucesso!");
        }
    }
}
