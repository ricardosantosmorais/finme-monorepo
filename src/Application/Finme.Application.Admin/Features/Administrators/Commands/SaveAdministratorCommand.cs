using Finme.Application.Admin.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.Features.Administrators.Commands
{
    public class SaveAdministratorCommand : IRequest<AdministratorDto>
    {
        public string FullName { get; set; }
        public string? Image { get; set; }
        public string? Telephone { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string Password { get; set; }
    }
}
