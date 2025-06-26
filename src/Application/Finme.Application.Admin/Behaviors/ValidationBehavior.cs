﻿using Finme.Application.Admin.Common;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Finme.Application.Admin.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    var errorResponse = new ErrorResponse(
                        title: "Validation Error",
                        status: 400,
                        detail: "One or more validation errors occurred."
                    );
                    foreach (var failure in failures)
                    {
                        errorResponse.AddError(failure.PropertyName, failure.ErrorMessage);
                    }

                    // Lançar ValidationException com os erros diretamente no construtor
                    throw new ValidationException(failures);
                }
            }
            return await next();
        }
    }
}
