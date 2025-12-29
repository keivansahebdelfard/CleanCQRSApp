using FluentValidation;
using MediatR;
using MyApp.Application.Common.Results;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Common.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse>
     : IPipelineBehavior<TRequest, TResponse>
     where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var errors = _validators
                    .SelectMany(v => v.Validate(context).Errors)
                    .Where(e => e != null)
                    .Select(e => new Error(
                        "ValidationError",
                        e.ErrorMessage))
                    .ToList();

                if (errors.Any())
                {
                    var resultType = typeof(TResponse);

                    if (resultType == typeof(Result))
                        return (TResponse)(object)Result.Failure(errors.First());

                    if (resultType.IsGenericType &&
                        resultType.GetGenericTypeDefinition() == typeof(Result<>))
                    {
                        var failureMethod = resultType
                            .GetMethod(nameof(Result<object>.Failure));

                        return (TResponse)failureMethod!
                            .Invoke(null, new object[] { errors.First() })!;
                    }
                }
            }

            return await next();
        }
    }
}
