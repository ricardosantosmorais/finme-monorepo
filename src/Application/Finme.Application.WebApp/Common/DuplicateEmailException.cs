using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.WebApp.Common
{
    public class DuplicateEmailException(string email) : Exception($"Já existe uma conta com o e-mail {email}.")
    {
    }
}
