using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CodeStream.logDNA;
using CodeStream.MediatR;
using MediatR;
using NExtensions;

namespace CodeStresmAspNetCoreApiStarter.Infrastructure.MediatR
{
    public class LogDNAMediatrPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogDNALogger logger;
        public LogDNAMediatrPipeline(ILogDNALogger logger)
        {
            this.logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var timer = new Stopwatch();
            timer.Start();

            var result = await next();

            timer.Stop();
            Log(request, result, timer.ElapsedMilliseconds);

            return result;
        }

        private void Log(TRequest req, TResponse res, long executionTimeInMilliseconds)
        {
            var obj = new LogDNAMeta<TRequest>(req)
            {
                MessageSucceeded = true, //default to true
                ExecutionMilliseconds = executionTimeInMilliseconds
            };

            var result = res as Result;
            if (result != null)
            {
                obj.MessageSucceeded = result.IsSuccess;
                obj.MessageFailures = result.Failures.JoinWithComma();
            }

            logger.LogObjectInfo(obj);

        }
    }
}