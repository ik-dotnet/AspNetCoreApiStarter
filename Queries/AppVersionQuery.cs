using CodeStresmAspNetCoreApiStarter.Infrastructure;
using CodeStresmAspNetCoreApiStarter.ViewModels;
using MediatR;

namespace CodeStresmAspNetCoreApiStarter.Queries
{
    public class AppVersionQuery : AppMessage, IRequest<AppVersionViewModel>
    {
        
    }
}