using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Users.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Users.Handlers
{
    public class DeleteUserHandler(
        IRepository<User> userRepository,
        IMapper mapper) : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IRepository<User> _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(x => x.Id == request.Id);
            if (user == null)
                throw new Exception("Usuário não foi encontrado!");

            user.DeletedAt = DateTime.UtcNow;
            user.Status = "Inactive";
            user.Active = false; // Atualiza o campo Active para refletir o status inativo

            await _userRepository.SaveChangesAsync();

            return true;
        }
    }
}