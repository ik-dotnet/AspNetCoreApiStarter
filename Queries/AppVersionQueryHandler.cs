using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CodeStresmAspNetCoreApiStarter.ViewModels;
using MediatR;

namespace CodeStresmAspNetCoreApiStarter.Queries
{
    public class AppVersionQueryHandler : IRequestHandler<AppVersionQuery, AppVersionViewModel>
    {
        public async Task<AppVersionViewModel> Handle(AppVersionQuery request, CancellationToken cancellationToken)
        {
            return new AppVersionViewModel{Version = typeof(Program).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version };
        }
    }
}