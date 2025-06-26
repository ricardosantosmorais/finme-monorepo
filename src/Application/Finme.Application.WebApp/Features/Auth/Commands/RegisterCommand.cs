using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.WebApp.Features.Auth.Commands
{
    public class RegisterCommand : IRequest<int>
    {
        public string FullName { get; set; }
        public string Telephone { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
