using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CodeStresmAspNetCoreApiStarter.Infrastructure;
using MediatR;

namespace CodeStresmAspNetCoreApiStarter.ViewModels
{
    public class AppVersionResult
    {
        public string Version { get; set; }
    }

    public class AppVersionQuery : AppMessage, IRequest<AppVersionResult> { }

    public class AppVersionQueryHandler : IRequestHandler<AppVersionQuery, AppVersionResult>
    {
        public Task<AppVersionResult> Handle(AppVersionQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new AppVersionResult
            {
                Version = typeof(Program).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version
            });
        }
    }


}