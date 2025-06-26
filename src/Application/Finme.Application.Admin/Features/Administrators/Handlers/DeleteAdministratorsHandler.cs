using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Administrators.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Administrators.Handlers
{
    public class DeleteAdministratorsHandler(
        IRepository<Administrator> administratorRepository,
            IMapper mapper) : IRequestHandler<DeleteAdministratorCommand, bool>
    {
        private readonly IRepository<Administrator> _administratorRepository = administratorRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> Handle(DeleteAdministratorCommand request, CancellationToken cancellationToken)
        {
            var user = await _administratorRepository.FindAsync(x => x.Id == request.Id);
            if (user == null)
                throw new Exception("Usuário não foi encontrado!");

            user.DeletedAt = DateTime.UtcNow;
            user.Status = "Inactive";

            await _administratorRepository.SaveChangesAsync();

            return true;
        }
    }
}
