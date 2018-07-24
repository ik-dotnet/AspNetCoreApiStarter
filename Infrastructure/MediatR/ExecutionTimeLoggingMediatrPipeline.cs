using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CodeStresmAspNetCoreApiStarter.Infrastructure.MediatR
{
    public class ExecutionTimeLoggingMediatrPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var timer = new Stopwatch();
            timer.Start();

            var result = await next();

            timer.Stop();

            var appMsg = request as AppMessage;
            if (appMsg != null)
            {
                appMsg.ExecutionTimeInMilliseconds = timer.ElapsedMilliseconds;
            }

            return result;
        }
    }
}