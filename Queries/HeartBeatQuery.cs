using CodeStresmAspNetCoreApiStarter.Infrastructure;
using CodeStresmAspNetCoreApiStarter.ViewModels;
using MediatR;

namespace CodeStresmAspNetCoreApiStarter.Queries
{
    public class HeartBeatQuery : AppMessage, IRequest<HeartBeatViewModel>
    {
        
    }
}