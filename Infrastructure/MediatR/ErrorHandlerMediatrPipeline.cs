using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CodeStream.logDNA;
using MediatR;

namespace CodeStresmAspNetCoreApiStarter.Infrastructure.MediatR
{
    public class ErrorHandlerMediatrPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : AppMessage, IRequest<TResponse>
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
                logdna.LogObjectInfo(new LogDNAMeta<TRequest>(request)
                {
                    MessageSucceeded = false, 
                    MessageFailures = e.ToStringDemystified()
                });

                logdna.LogError(e, request.CorrelationId);
                throw new UnhandledEventingPipelineException(e, request.CorrelationId);
            }
        }
    }

    public class UnhandledEventingPipelineException : Exception
    {
        public UnhandledEventingPipelineException(Exception ex, string correlationId) : base($"An unhandled error occurred. Please check the logs for error ID: {correlationId}", ex) { }
    }

}