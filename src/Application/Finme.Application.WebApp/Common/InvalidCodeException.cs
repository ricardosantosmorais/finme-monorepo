using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.WebApp.Common
{
    public class InvalidCodeException() : Exception($"Código inválido ou expirado.")
    {
    }
}
