using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using Finme.Application.WebApp.DTOs;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Finme.Application.WebApp.Features.Account.Commands
{
    public class AccountDownloadCommandHandler(
        IRepository<User> userRepository,
        IRepository<UserDocument> userDocumentsRepository,
        IAmazonS3 s3Client,
        IConfiguration configuration,
        IMapper mapper) : IRequestHandler<AccountDownloadCommand, DownloadDocumentResponse>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IRepository<UserDocument> _userDocumentsRepository = userDocumentsRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IAmazonS3 _s3Client = s3Client;

        public async Task<DownloadDocumentResponse> Handle(AccountDownloadCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(u => u.Id == request.UserId);
            if (user == null)
                throw new Exception("Usuário não foi encontrado");

            var document = await _userDocumentsRepository.FindAsync(x => x.UserId == request.UserId && x.Id == request.DocumentId);
            if (document == null)
                throw new Exception("Documento não encontrado");

            var s3Request = new GetObjectRequest
            {
                BucketName = document.BucketName,
                Key = document.Key
            };

            using var response = await _s3Client.GetObjectAsync(s3Request, cancellationToken);

            // Copiar o ResponseStream para um MemoryStream
            var memoryStream = new MemoryStream();
            await response.ResponseStream.CopyToAsync(memoryStream, cancellationToken);
            memoryStream.Position = 0; // Resetar a posição do stream

            var contentType = response.Headers.ContentType ?? GetContentTypeFromExtension(document.Key);
            var fileName = document.Key.Split('/').Last();

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
