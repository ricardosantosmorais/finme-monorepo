using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Operations.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Operations.Handlers
{
    public class OperationAllCommandHandler(
            IRepository<Operation> operationRepository,
            IMapper mapper
        ) : IRequestHandler<OperationAllCommand, OperationDto[]>
    {
        private readonly IRepository<Operation> _operationRepository = operationRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<OperationDto[]> Handle(OperationAllCommand request, CancellationToken cancellationToken)
        {
            // Start with a base query
            var query = _operationRepository.GetQueryable();

            // Apply Name filter if provided
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(o => o.Name.ToLower().Contains(request.Name.ToLower()));
            }

            // Apply Type filter if provided
            if (!string.IsNullOrWhiteSpace(request.Type))
            {
                var arrayType = request.Type.Split(',');
                query = query.Where(o => arrayType.Contains(o.Type));
            }

            // Apply Category filter if provided
            if (!string.IsNullOrWhiteSpace(request.Category))
            {
                var arrayCategory = request.Category.Split(',');
                query = query.Where(o => arrayCategory.Contains(o.Category));
            }

            if (!string.IsNullOrWhiteSpace(request.Status))
            {
                var arrayStatus = request.Status.Split(',');
                query = query.Where(o => arrayStatus.Contains(o.Status));
            }

            // Execute the query and map to DTOs
            var operations = await _operationRepository.ToListAsync(query, cancellationToken);
            return _mapper.Map<OperationDto[]>(operations);
        }
    }
}
