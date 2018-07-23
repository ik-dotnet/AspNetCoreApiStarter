using System.Threading.Tasks;
using CodeStresmAspNetCoreApiStarter.Queries;
using CodeStresmAspNetCoreApiStarter.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeStresmAspNetCoreApiStarter.Controllers
{
    [ApiController]
    public class UtilityController
    {
        private readonly IMediator mediatr;

        public UtilityController(IMediator mediatr)
        {
            this.mediatr = mediatr;
        }

        [HttpGet("version")]
        public async Task<ActionResult<AppVersionViewModel>> GetVersion()
        {
            return new ActionResult<AppVersionViewModel>(await mediatr.Send(new AppVersionQuery()));
        }

    }
}