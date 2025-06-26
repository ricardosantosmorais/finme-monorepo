using Amazon.Runtime.Internal;
using AutoMapper;
using Finme.Application.Admin.Common;
using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Administrators.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.Features.Administrators.Handlers
{
    public class SaveAdministratorHandler(
        IRepository<Administrator> administratorRepository,
        IMapper mapper
        ) : IRequestHandler<SaveAdministratorCommand, AdministratorDto>
    {
        private readonly IRepository<Administrator> _administratorRepository = administratorRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<AdministratorDto> Handle(SaveAdministratorCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _administratorRepository.FindAsync(u => u.Email == command.Email);
            if (existingUser != null)
                throw new DuplicateEmailException(command.Email);

            var existingUserCPF = await _administratorRepository.FindAsync(u => u.Cpf == command.Cpf);
            if (existingUserCPF != null)
                throw new DuplicateCpfException(command.Cpf);

            // Criptografar a senha com BCrypt
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);

            // Criar o usuário
            var user = _mapper.Map<Administrator>(command);

            user.CreatedAt = DateTime.UtcNow;
            user.Status = "active";
            user.Active = command.Active;

            await _administratorRepository.AddAsync(user);
            await _administratorRepository.SaveChangesAsync();

            return _mapper.Map<AdministratorDto>(user);
        }
    }
}
