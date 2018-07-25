using System.Threading.Tasks;
using CodeStresmAspNetCoreApiStarter.Infrastructure;
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
        public async Task<ActionResult<AppVersion>> GetVersion()
        {
            return (await mediatr.Send(new AppVersion.Query())).AsActionResult();
        }

        /// <summary>
        /// Get Heartbeat of the application (default application url)
        /// </summary>
        [HttpGet("")] 
        public async Task<ActionResult<HeartBeat>> GetHeartBeat()
        {
            return (await mediatr.Send(new HeartBeat.Query())).AsActionResult();
        }

    }
}