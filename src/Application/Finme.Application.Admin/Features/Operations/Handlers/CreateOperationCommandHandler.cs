using AutoMapper;
using Finme.Application.Admin.DTOs;
using Finme.Application.Admin.Features.Operations.Commands;
using Finme.Application.Admin.Helpers;
using Finme.Domain.Entities;
using Finme.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finme.Application.Admin.Features.Operations.Handlers
{
    // O handler agora declara que retorna um ResultDto contendo um inteiro (o ID)
    public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand, ResultDto<int>>
    {
        private readonly IRepository<Operation> _operationRepository;
        private readonly IMapper _mapper;

        public CreateOperationCommandHandler(IRepository<Operation> operationRepository, IMapper mapper)
        {
            _operationRepository = operationRepository;
            _mapper = mapper;
        }

        public async Task<ResultDto<int>> Handle(CreateOperationCommand request, CancellationToken cancellationToken)
        {
            // Validação de exemplo (aqui você poderia usar FluentValidation)
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return ResultDto<int>.FailureResult("O nome da operação é obrigatório.");
            }

            // 1. Gera o alias base a partir do nome da operação
            var baseAlias = SlugHelper.GenerateSlug(request.Name);
            var finalAlias = baseAlias;
            int counter = 1;

            // 2. Verifica a unicidade no banco de dados e adiciona um sufixo se necessário
            while (await _operationRepository.FindAsync(o => o.Alias == finalAlias) != null)
            {
                counter++;
                finalAlias = $"{baseAlias}-{counter}";
            }

            var operation = _mapper.Map<Operation>(request);

            operation.Alias = finalAlias; // Define o alias único
            operation.Active = true;
            operation.CreatedAt = DateTime.UtcNow;

            await _operationRepository.AddAsync(operation);
            await _operationRepository.SaveChangesAsync();

            // Retorna um resultado de sucesso com o ID da nova operação
            return ResultDto<int>.SuccessResult(operation.Id, "Operação criada com sucesso!");
        }
    }
}
