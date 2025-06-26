using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.DTOs
{
    public class ConfirmVerificationCodeDto
    {
        public int UserId { get; set; }
        public string Code { get; set; }
    }
}
