using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Finme.Application.Admin.Features.Account.Commands
{
    public class AccountDocumentsCommandHandler(
        IRepository<User> userRepository,
        IRepository<UserDocument> userDocumentsRepository,
        IAmazonS3 s3Client,
        IConfiguration configuration,
        IMapper mapper) : IRequestHandler<AccountDocumentsCommand, UserDto>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IRepository<UserDocument> _userDocumentsRepository = userDocumentsRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IAmazonS3 _s3Client = s3Client;
        private readonly string _bucketName = configuration["AWS:S3:BucketName"];

        public async Task<UserDto> Handle(AccountDocumentsCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithDependenciesAsync(u => u.Id == request.Id);
            if (user == null)
                throw new Exception("Usuário não foi encontrado");

            if (string.IsNullOrEmpty(_bucketName))
            {
                throw new Exception("Nome do bucket S3 não configurado.");
            }

            var uploadedFiles = new List<string>();

            for (int i = 0; i < request.Files.Count; i++)
            {
                var file = request.Files[i];

                if (file.Length == 0)
                {
                    throw new Exception($"Arquivo {file.FileName} está vazio.");
                }

                // Gerar um nome único para o arquivo
                var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                var key = $"documents/{request.Id}/{uniqueFileName}"; // Organiza por userId

                // Configurar metadados para o S3
                var metadata = new Dictionary<string, string>
                {
                    { "Content-Type", request.ContentTypes[i] },
                    { "Document-Category", request.DocumentTypes[i] }
                };

                // Configurar a requisição de upload para o S3
                var putRequest = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = key,
                    InputStream = file.OpenReadStream(),
                    ContentType = request.ContentTypes[i]
                };

                // Fazer o upload do arquivo para o S3
                var response = await _s3Client.PutObjectAsync(putRequest, cancellationToken);

                if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception($"Falha ao enviar o arquivo {file.FileName} para o S3.");
                }

                await _userDocumentsRepository.AddAsync(new UserDocument
                {
                    DocumentType = request.DocumentTypes[i],
                    Type = request.ContentTypes[i],
                    UserId = request.Id,
                    Key = key,
                    FileName = uniqueFileName,
                    BucketName = _bucketName,
                    CreatedAt = DateTime.UtcNow,
                    Active = true
                });

                uploadedFiles.Add(uniqueFileName);
            }

            await _userDocumentsRepository.SaveChangesAsync();

            // Retornar o DTO com os dados do usuário e o token
            return _mapper.Map<UserDto>(user);
        }
    }
}
