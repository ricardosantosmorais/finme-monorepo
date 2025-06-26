using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.Features.Auth.Commands
{
    public class ChangePasswordCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
    }
}
