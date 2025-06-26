using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Administrators.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Administrators.Handlers
{
    public class GetAllAdministratorsHandler(
        IRepository<Administrator> administratorRepository,
            IMapper mapper) : IRequestHandler<GetAllAdministratorsCommand, List<AdministratorDto>>
    {
        private readonly IRepository<Administrator> _administratorRepository = administratorRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<AdministratorDto>> Handle(GetAllAdministratorsCommand request, CancellationToken cancellationToken)
        {
            var all = await _administratorRepository.Get(x => !x.DeletedAt.HasValue);
            return _mapper.Map<List<AdministratorDto>>(all);
        }
    }
}
