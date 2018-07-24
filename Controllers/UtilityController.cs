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

        /// <summary>
        /// Get Application Version
        /// </summary>
        [HttpGet("version")]
        public async Task<ActionResult<AppVersionViewModel>> GetVersion()
        {
            return new ActionResult<AppVersionViewModel>(await mediatr.Send(new AppVersionQuery()));
        }

        /// <summary>
        /// Get Heartbeat of the application (default application url)
        /// </summary>
        [HttpGet("")] 
        public async Task<ActionResult<HeartBeatViewModel>> GetHeartBeat()
        {
            return new ActionResult<HeartBeatViewModel>(await mediatr.Send(new HeartBeatQuery()));
        }

    }
}