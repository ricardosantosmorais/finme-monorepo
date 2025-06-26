using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.Features.Administrators.Commands
{
    public class DeleteAdministratorCommand : IRequest<bool>
    {
        public int Id {  get; set; }
    }
}
