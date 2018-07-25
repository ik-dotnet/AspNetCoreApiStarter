using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CodeStresmAspNetCoreApiStarter.Infrastructure;
using MediatR;

namespace CodeStresmAspNetCoreApiStarter.ViewModels
{
    public class AppVersion
    {
        public string Version { get; set; }

        public class Query : AppMessage, IRequest<AppVersion> { }

        public class Handler : IRequestHandler<Query, AppVersion>
        {
            public Task<AppVersion> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new AppVersion
                {
                    Version = typeof(Program).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version
                });
            }
        }

    }


}