using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Account.Commands
{
    public class AccountPersonaCommandHandler(
        IRepository<User> userRepository,
        IMapper mapper) : IRequestHandler<AccountPersonaCommand, UserDto>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<UserDto> Handle(AccountPersonaCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithDependenciesAsync(u => u.Id == request.UserId);
            if (user == null)
                throw new Exception("Usuário não foi encontrado");

            if (request.DateOfBirth.HasValue)
            {
                user.DateOfBirth = DateTime.SpecifyKind(request.DateOfBirth.Value, DateTimeKind.Utc);
            }
            else
            {
                user.DateOfBirth = null;
            }
            user.DocumentNumber = request.DocumentNumber;
            user.Nationality = request.Nationality;
            user.Naturalness = request.Naturalness;
            user.Gender = request.Gender;
            user.MaritalStatus = request.MaritalStatus;
            user.CpfSpouse = request.CpfSpouse;
            user.FullNameSpouse = request.FullNameSpouse;
            user.UpdatedAt = DateTime.UtcNow;

            // Persistir no banco
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            // Retornar o DTO com os dados do usuário e o token
            return _mapper.Map<UserDto>(user);
        }
    }
}
