using AutoMapper;
using Finme.Application.WebApp.DTOs;
using Finme.Application.WebApp.Features.Operations.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.WebApp.Features.Operations.Handlers
{
    public class OperationCommandHandler(
            IRepository<Operation> operationRepository,
            IMapper mapper
        ) : IRequestHandler<OperationCommand, OperationDto>
    {
        private readonly IRepository<Operation> _operationRepository = operationRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<OperationDto> Handle(OperationCommand request, CancellationToken cancellationToken)
        {
            var operation = await _operationRepository.GetOperationWithDependenciesAsync(x => x.Alias == request.Alias);
            if (operation == null)
                throw new Exception("Operação não foi encontrada");

            if (operation.Files != null && operation.Files.Any())
            {
                operation.Files = [.. operation.Files.Where(x => !x.DeletedAt.HasValue && x.Active && x.DocumentType == "document")];
            }

            if (operation.Comments != null && operation.Comments.Any())
            {
                operation.Comments = [.. operation.Comments.Where(x => !x.DeletedAt.HasValue && x.Active)];
            }
            return _mapper.Map<OperationDto>(operation);
        }
    }
}
