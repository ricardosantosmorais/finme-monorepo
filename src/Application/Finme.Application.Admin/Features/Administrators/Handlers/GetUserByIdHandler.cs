using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Administrators.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Administrators.Handlers
{
    public class GetUserByIdHandler(
        IRepository<Administrator> administratorRepository,
            IMapper mapper) : IRequestHandler<GetAdministratorByIdCommand, AdministratorDto>
    {
        private readonly IRepository<Administrator> _administratorRepository = administratorRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<AdministratorDto> Handle(GetAdministratorByIdCommand request, CancellationToken cancellationToken)
        {
            var all = await _administratorRepository.GetByIdAsync(request.Id);
            return _mapper.Map<AdministratorDto>(all);
        }
    }
}
