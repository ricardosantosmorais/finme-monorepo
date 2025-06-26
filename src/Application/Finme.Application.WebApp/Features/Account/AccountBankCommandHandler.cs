using AutoMapper;
using Finme.Application.WebApp.DTOs;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.WebApp.Features.Account.Commands
{
    public class AccountBankCommandHandler(
        IRepository<User> userRepository,
        IRepository<UserBank> userBankRepository,
        IMapper mapper) : IRequestHandler<AccountBankCommand, UserResponseDto>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IRepository<UserBank> _userBankRepository = userBankRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<UserResponseDto> Handle(AccountBankCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithDependenciesAsync(u => u.Id == request.UserId);
            if (user == null)
                throw new Exception("Usuário não foi encontrado");

            var banks = await _userBankRepository.Get(x => x.UserId == user.Id);
            user.UserBanks = [.. banks];

            if (banks.Any())
            {
                foreach (var item in user.UserBanks)
                {
                    _userBankRepository.Delete(item);
                }
            }

            user.UserBanks = [];

            await _userBankRepository.AddAsync(new UserBank
            {
                Agency = request.Agency,
                Bank = request.Bank,
                CpfSpouse = request.CpfSpouse,
                CurrentAccount = request.CurrentAccount,
                FullNameSpouse = request.FullNameSpouse,
                JoinAccount = request.JoinAccount,
                UserId = user.Id,
            });
            await _userBankRepository.SaveChangesAsync();

            // Retornar o DTO com os dados do usuário e o token
            return _mapper.Map<UserResponseDto>(user);
        }
    }
}
