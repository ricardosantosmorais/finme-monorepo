using Amazon.S3;
using Amazon.S3.Model;
using Finme.Application.WebApp.DTOs;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Finme.Application.WebApp.Features.Investments.Commands;
using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using Finme.Application.WebApp.Helpers;

// Ajuste o namespace se necessário
namespace Finme.Application.WebApp.Features.Operations.Handlers
{
    public class InvestmentContractHandler(
        IRepository<Operation> operationRepository,
        IRepository<OperationFile> operationDocumentsRepository,
        IAmazonS3 s3Client,
        IConfiguration configuration) : IRequestHandler<InvestmentContractCommand, DownloadDocumentResponse>
    {
        // ... (propriedades injetadas permanecem as mesmas)
        private readonly IRepository<Operation> _operationRepository = operationRepository;
        private readonly IRepository<OperationFile> _operationDocumentsRepository = operationDocumentsRepository;
        private readonly IAmazonS3 _s3Client = s3Client;
        private readonly string _bucketName = configuration["AWS:S3:BucketNameOperation"];

        public async Task<DownloadDocumentResponse> Handle(InvestmentContractCommand request, CancellationToken cancellationToken)
        {
            var operation = await _operationRepository.FindAsync(u => u.Id == request.OperationId);
            if (operation == null)
                throw new Exception("Operação não foi encontrada");

            if (string.IsNullOrEmpty(_bucketName))
                throw new Exception("Nome do bucket S3 não configurado.");

            var document = await _operationDocumentsRepository.FindAsync(x => x.OperationId == request.OperationId && x.Id == Convert.ToInt32(operation.ContractFile));
            if (document == null)
                throw new Exception("Documento não encontrado");

            // Baixa o PDF da S3 para a memória
            var s3Request = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = document.Key
            };
            using var response = await _s3Client.GetObjectAsync(s3Request, cancellationToken);
            var originalPdfStream = new MemoryStream();
            await response.ResponseStream.CopyToAsync(originalPdfStream, cancellationToken);
            // NÃO precisa resetar a posição do stream para o iText
            originalPdfStream.Position = 0;
            // Modifica o PDF em memória usando iText 7
            MemoryStream modifiedPdfStream = ModifyPdfInMemory.Execute(originalPdfStream, operation);

            // Prepara a resposta com o PDF modificado
            var fileName = $"Contrato_{operation.Id}_iText.pdf";
            return new DownloadDocumentResponse
            {
                FileStream = modifiedPdfStream,
                FileName = fileName,
                ContentType = "application/pdf"
            };
        }
    }
}