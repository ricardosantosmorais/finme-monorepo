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
    public class ChangeAdministratorHandler(
        IRepository<Administrator> administratorRepository,
        IMapper mapper
        ) : IRequestHandler<ChangeAdministratorCommand, AdministratorDto>
    {
        private readonly IRepository<Administrator> _administratorRepository = administratorRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<AdministratorDto> Handle(ChangeAdministratorCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _administratorRepository.FindAsync(u => u.Email == command.Email || u.Cpf == command.Cpf);
            if (existingUser != null && existingUser.Id != command.Id)
                throw new DuplicateEmailException(command.Email);

            var existingUserCPF = await _administratorRepository.FindAsync(u => u.Cpf == command.Cpf);
            if (existingUserCPF != null && existingUserCPF.Id != command.Id)
                throw new DuplicateCpfException(command.Cpf);

            // Criar o usuário
            var user = await _administratorRepository.FindAsync(x => x.Id == command.Id);

            if(user == null)
                throw new Exception("Usuário não encontrado!");

            user.Cpf = command.Cpf;
            user.UpdateEmail(command.Email);
            user.FullName = command.FullName;
            user.Image = command.Image;
            user.Telephone = command.Telephone;
            user.Active = command.Active;
            user.Status = command.Status;

            _administratorRepository.Update(user);
            await _administratorRepository.SaveChangesAsync();

            return _mapper.Map<AdministratorDto>(user);
        }
    }
}
