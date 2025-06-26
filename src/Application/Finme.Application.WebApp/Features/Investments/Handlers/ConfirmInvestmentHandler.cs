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
using Finme.Domain.Enums;

// Ajuste o namespace se necessário
namespace Finme.Application.WebApp.Features.Operations.Handlers
{
    public class ConfirmInvestmentHandler(
        IRepository<Operation> operationRepository,
        IRepository<Investment> investmentRepository,
        IRepository<InvestmentStatus> investmentStatusRepository,
        IRepository<User> userRepository,
        IRepository<VerificationCode> verificationCodeRepository,
        IRepository<OperationFile> operationDocumentsRepository,
        IRepository<UserDocument> userDocumentsRepository,
        IAmazonS3 s3Client,
        IConfiguration configuration) : IRequestHandler<ConfirmInvestmentCommand, ResultDto<int>>
    {
        private readonly IRepository<Operation> _operationRepository = operationRepository;
        private readonly IRepository<OperationFile> _operationDocumentsRepository = operationDocumentsRepository;
        private readonly IRepository<VerificationCode> _verificationCodeRepository = verificationCodeRepository;
        private readonly IRepository<Investment> _investmentRepository = investmentRepository;
        private readonly IRepository<InvestmentStatus> _investmentStatusRepository = investmentStatusRepository;
        private readonly IRepository<UserDocument> _userDocumentsRepository = userDocumentsRepository;
        private readonly IRepository<User> _userRepository = userRepository;

        private readonly IAmazonS3 _s3Client = s3Client;
        private readonly string _bucketNameOperation = configuration["AWS:S3:BucketNameOperation"];
        private readonly string _bucketNameUser = configuration["AWS:S3:BucketName"];

        public async Task<ResultDto<int>> Handle(ConfirmInvestmentCommand request, CancellationToken cancellationToken)
        {
            var verificationCode = await _verificationCodeRepository.FindAsync(vc =>
               vc.UserId == request.UserId &&
               vc.Code == request.Code &&
               vc.OperationId == request.OperationId &&
               vc.InvestmentId == request.InvestmentId &&
               vc.ExpirationTime > DateTime.UtcNow);

            if (verificationCode == null)
            {
                throw new Exception("Código de confirmação inválido ou expirado");
            }

            var operation = await _operationRepository.GetByIdAsync(request.OperationId);
            if (operation == null)
                throw new Exception("Operação não foi encontrada");

            // 2. Buscar o investimento existente no banco de dados
            var investment = await _investmentRepository.GetByIdAsync(request.InvestmentId);

            if (investment == null)
            {
                throw new InvalidOperationException($"Investimento com ID {request.InvestmentId} não foi encontrado.");
            }

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
                throw new Exception("Usuário não encontrado");

            if (string.IsNullOrEmpty(_bucketNameOperation))
                throw new Exception("Nome do bucket S3 não configurado.");

            var document = await _operationDocumentsRepository.FindAsync(x => x.OperationId == request.OperationId && x.Id == Convert.ToInt32(operation.ContractFile));
            if (document == null)
                throw new Exception("Documento não encontrado");

            //Salvando o novo status
            var newStatus = new InvestmentStatus
            {
                InvestmentId = investment.Id,
                Status = ETransactionStatus.PendingFunds, // Usamos o enum que foi convertido
                CreatedAt = DateTime.UtcNow
            };

            await _investmentStatusRepository.AddAsync(newStatus);
            await _investmentStatusRepository.SaveChangesAsync();

            /*
            operation.Investors += 1; // Incrementa o número de investidores
            operation.UpdatedAt = DateTime.UtcNow;
            operation.AmountCollected += investment.ConfirmedValue; // Atualiza o valor coletado

            await _operationRepository.UpdateAsync(operation);
            await _operationRepository.SaveChangesAsync();
            */

            // Baixa o PDF da S3 para a memória
            var s3Request = new GetObjectRequest
            {
                BucketName = _bucketNameOperation,
                Key = document.Key
            };
            using var responseFile = await _s3Client.GetObjectAsync(s3Request, cancellationToken);
            var originalPdfStream = new MemoryStream();
            await responseFile.ResponseStream.CopyToAsync(originalPdfStream, cancellationToken);
            // NÃO precisa resetar a posição do stream para o iText
            originalPdfStream.Position = 0;
            // Modifica o PDF em memória usando iText 7
            MemoryStream modifiedPdfStream = ModifyPdfInMemory.Execute(originalPdfStream, operation);

            // Gerar um nome único para o arquivo
            var uniqueFileName = $"{Guid.NewGuid()}_{investment.Id}";
            var key = $"documents/{request.UserId}/{uniqueFileName}"; // Organiza por userId

            // Configurar a requisição de upload para o S3
            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketNameUser,
                Key = key,
                InputStream = modifiedPdfStream,
                ContentType = "application/pdf"
            };

            // Fazer o upload do arquivo para o S3
            var response = await _s3Client.PutObjectAsync(putRequest, cancellationToken);

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Falha ao enviar o arquivo {uniqueFileName} para o S3.");
            }

            var contractFile = new UserDocument
            {
                DocumentType = "contract",
                Type = "application/pdf",
                UserId = request.UserId,
                Key = key,
                FileName = $"{uniqueFileName}.pdf",
                BucketName = _bucketNameUser,
                CreatedAt = DateTime.UtcNow,
                Active = true
            };

            await _userDocumentsRepository.AddAsync(contractFile);
            await _userDocumentsRepository.SaveChangesAsync();

            investment.UpdatedAt = DateTime.UtcNow;
            //investment.ConfirmedValue = investment.CommitedValue;
            investment.StatusId = newStatus.Id;
            investment.ContractFile = contractFile.Id.ToString();
            
            await _investmentRepository.SaveChangesAsync();

            return ResultDto<int>.SuccessResult(investment.Id, "Investimento atualizado com sucesso!");
        }
    }
}