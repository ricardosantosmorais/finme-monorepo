using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.Common
{
    public class DuplicateCpfException(string cpf) : Exception($"Já existe uma conta com o CPF {cpf}.")
    {
    }
}
