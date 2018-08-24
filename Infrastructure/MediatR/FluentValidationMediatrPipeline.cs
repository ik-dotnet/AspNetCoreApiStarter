using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodeStream.MediatR;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CodeStresmAspNetCoreApiStarter.Infrastructure.MediatR
{
    public class FluentValidationMediatrPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result, new()
    {
        private readonly IValidator<TRequest>[] validators;

        public FluentValidationMediatrPipeline(IValidator<TRequest>[] validators) => this.validators = validators;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext(request);

            var results = new List<ValidationResult>();
            foreach (var validator in validators)
            {
                results.Add(await validator.ValidateAsync(context, cancellationToken));
            }

            var failures = results.SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                var result = new TResponse();
                result.SetFailures(failures.Select(f => f.ErrorMessage).ToList());
                return result;
            }

            return await next();
        }
    }
}