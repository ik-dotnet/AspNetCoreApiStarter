using System.Threading.Tasks;
using CodeStresmAspNetCoreApiStarter.Infrastructure;
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
            return (await mediatr.Send(new AppVersionQuery())).AsActionResult();
        }

        /// <summary>
        /// Get Heartbeat of the application (default application url)
        /// </summary>
        [HttpGet("")] 
        public async Task<ActionResult<HeartBeatViewModel>> GetHeartBeat()
        {
            return (await mediatr.Send(new HeartBeatQuery())).AsActionResult();
        }

    }
}