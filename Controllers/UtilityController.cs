using System.Threading.Tasks;
using CodeStresmAspNetCoreApiStarter.Queries;
using CodeStresmAspNetCoreApiStarter.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeStresmAspNetCoreApiStarter.Controllers
{
    public class UtilityController
    {
        private readonly IMediator mediatr;

        public UtilityController(IMediator mediatr)
        {
            this.mediatr = mediatr;
        }

        [HttpGet("version")]
        public async Task<ActionResult<VersionViewModel>> GetVersion()
        {
            return new ActionResult<VersionViewModel>(await mediatr.Send(new VersionQuery()));
        }

    }
}