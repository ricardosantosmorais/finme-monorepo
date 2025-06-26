using Amazon.Runtime.Internal;
using AutoMapper;
using Finme.Application.Admin.Common;
using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Administrators.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Administrators.Handlers
{
    public class ChangeAdministratorPasswordHandler(
        IRepository<Administrator> administratorRepository,
        IMapper mapper
        ) : IRequestHandler<ChangeAdministratorPasswordCommand, AdministratorDto>
    {
        private readonly IRepository<Administrator> _administratorRepository = administratorRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<AdministratorDto> Handle(ChangeAdministratorPasswordCommand command, CancellationToken cancellationToken)
        {
            // consulta o usuário
            var user = await _administratorRepository.FindAsync(x => x.Id == command.Id);

            if(user == null)
                throw new Exception("Usuário não encontrado!");

            // Criptografar a senha com BCrypt
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);
            user.UpdatedAt = DateTime.UtcNow;

            _administratorRepository.Update(user);
            await _administratorRepository.SaveChangesAsync();

            return _mapper.Map<AdministratorDto>(user);
        }
    }
}
