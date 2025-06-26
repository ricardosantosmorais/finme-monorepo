using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.Common
{
    public class ErrorDetails(string propertyName, string message)
    {
        public string PropertyName { get; set; } = propertyName;
        public string Message { get; set; } = message;
    }
}
