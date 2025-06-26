using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Finme.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Finme.Infrastructure.Services
{
    public class VerificationService : IVerificationService
    {
        private readonly IAmazonSimpleNotificationService _snsClient;
        private readonly IAmazonSimpleEmailService _sesClient;
        private readonly string _senderEmail;

        public VerificationService(
            IAmazonSimpleNotificationService snsClient,
            IAmazonSimpleEmailService sesClient,
            IConfiguration configuration)
        {
            _snsClient = snsClient;
            _sesClient = sesClient;
            _senderEmail = configuration["AWS:SES:SenderEmail"];
        }

        public async Task EnviarCodigoPorSMS(string phoneNumber, string code)
        {
            var request = new PublishRequest
            {
                PhoneNumber = phoneNumber,
                Message = $"{code} é seu código de verificação da Finme."
            };
            await _snsClient.PublishAsync(request);
        }

        public async Task EnviarCodigoPorEmail(string email, string code)
        {
            var request = new SendEmailRequest
            {
                Source = _senderEmail,
                Destination = new Destination { ToAddresses = new List<string> { email } },
                Message = new Message
                {
                    Subject = new Content("Código de Verificação"),
                    Body = new Body { Text = new Content($"Seu código de verificação é: {code}") }
                }
            };
            await _sesClient.SendEmailAsync(request);
        }
    }
}
