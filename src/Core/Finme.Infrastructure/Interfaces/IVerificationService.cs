using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Infrastructure.Interfaces
{
    public interface IVerificationService
    {
        Task EnviarCodigoPorSMS(string phoneNumber, string code);
        Task EnviarCodigoPorEmail(string email, string code);
    }
}
