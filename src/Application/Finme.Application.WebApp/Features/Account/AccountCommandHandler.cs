using AutoMapper;
using Finme.Application.WebApp.DTOs;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.WebApp.Features.Account.Commands
{
    public class AccountCommandHandler(
        IRepository<User> userRepository, 
        IRepository<UserBank> userBankRepository,
        IRepository<UserAddress> userAddressRepository,
        IRepository<UserDocument> userDocumentRepository,
        IMapper mapper) : IRequestHandler<AccountCommand, UserResponseDto>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IRepository<UserAddress> _userAddressRepository = userAddressRepository;
        private readonly IRepository<UserBank> _userBankRepository = userBankRepository;
        private readonly IRepository<UserDocument> _userDocumentRepository = userDocumentRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<UserResponseDto> Handle(AccountCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithDependenciesAsync(u => u.Id == command.Id);
            // Retornar o DTO com os dados do usuário e o token
            return _mapper.Map<UserResponseDto>(user);
        }
    }
}
