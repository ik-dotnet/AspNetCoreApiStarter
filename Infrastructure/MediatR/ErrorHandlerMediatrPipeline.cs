using System;
using System.Threading;
using System.Threading.Tasks;
using CodeStream.logDNA;
using MediatR;

namespace CodeStresmAspNetCoreApiStarter.Infrastructure.MediatR
{
    public class ErrorHandlerMediatrPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogDNALogger logdna;

        public ErrorHandlerMediatrPipeline(ILogDNALogger logdna)
        {
            this.logdna = logdna;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception e)
            {
                logdna.LogObjectError(new LogDNAMeta<Exception>(e));
                throw e.GetBaseException();
            }
        }
    }
}