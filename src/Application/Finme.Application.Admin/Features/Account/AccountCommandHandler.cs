using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Account.Commands
{
    public class AccountCommandHandler(
        IRepository<Administrator> administratorRepository, 
        IMapper mapper) : IRequestHandler<AccountCommand, AdministratorDto>
    {
        private readonly IRepository<Administrator> _administratorRepository = administratorRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<AdministratorDto> Handle(AccountCommand command, CancellationToken cancellationToken)
        {
            var user = await _administratorRepository.FindAsync(u => u.Id == command.Id);
            // Retornar o DTO com os dados do usuário e o token
            return _mapper.Map<AdministratorDto>(user);
        }
    }
}
