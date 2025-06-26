using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.WebApp.Features.Auth.Commands
{
    public class SendVerificationCodeCommand : IRequest
    {
        public int UserId { get; set; }
        public string Channel { get; set; } // "SMS" ou "Email"
    }
}
