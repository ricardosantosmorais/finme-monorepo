using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Operations.Commands;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;

namespace Finme.Application.Admin.Features.Operations.Handlers
{
    public class UpdateOperationCommandHandler : IRequestHandler<UpdateOperationCommand, ResultDto<int>>
    {
        private readonly IRepository<Operation> _operationRepository;
        private readonly IMapper _mapper;

        public UpdateOperationCommandHandler(IRepository<Operation> operationRepository, IMapper mapper)
        {
            _operationRepository = operationRepository;
            _mapper = mapper;
        }

        public async Task<ResultDto<int>> Handle(UpdateOperationCommand request, CancellationToken cancellationToken)
        {
            // 1. Busca a entidade existente no banco
            var operation = await _operationRepository.GetByIdAsync(request.Id); // Supondo um método GetByIdAsync

            if (operation == null)
            {
                return ResultDto<int>.FailureResult("Operação não encontrada.");
            }

            // 2. A MÁGICA ACONTECE AQUI: Mapeia o command (source) PARA a entidade já existente (destination)
            _mapper.Map(request, operation);

            // 3. Define os valores controlados pelo servidor na atualização
            operation.UpdatedAt = DateTime.UtcNow;

            // 4. Salva as alterações no repositório
            _operationRepository.Update(operation); // Supondo um método UpdateAsync

            await _operationRepository.SaveChangesAsync();

            return ResultDto<int>.SuccessResult(operation.Id, "Operação criada com sucesso!");
        }
    }
}
