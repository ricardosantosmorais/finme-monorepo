using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Account.Commands
{
    public class AccountChangePasswordCommandHandler(IRepository<User> userRepository) : IRequestHandler<AccountChangePasswordCommand, int>
    {
        private readonly IRepository<User> _userRepository = userRepository;

        public async Task<int> Handle(AccountChangePasswordCommand request, CancellationToken cancellationToken)
        {
            // Verificar se o email já existe (opcional, para evitar duplicatas)
            var existingUser = await _userRepository.FindAsync(u => u.Id == request.UserId);
            if (existingUser == null)
                throw new Exception("Usuário não foi encontrado");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, existingUser.Password))
                throw new Exception("A senha informada não confere com a senha atual");

            // Criptografar a senha com BCrypt
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.ConfirmPassword);

            // atualiza o usuário
            existingUser.UpdatePassword(passwordHash);

            // Persistir no banco
            _userRepository.Update(existingUser);
            await _userRepository.SaveChangesAsync();

            return existingUser.Id;
        }
    }
}
