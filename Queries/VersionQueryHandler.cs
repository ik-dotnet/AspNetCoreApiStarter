using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CodeStresmAspNetCoreApiStarter.ViewModels;
using MediatR;

namespace CodeStresmAspNetCoreApiStarter.Queries
{
    public class VersionQueryHandler : IRequestHandler<VersionQuery, VersionViewModel>
    {
        public async Task<VersionViewModel> Handle(VersionQuery request, CancellationToken cancellationToken)
        {
            return new VersionViewModel{Version = typeof(Program).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version };
        }
    }
}