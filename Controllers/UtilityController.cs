using System;
using System.Threading.Tasks;
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
        /// Get Heartbeat of the application (default application url)
        /// </summary>
        [HttpGet("")]
        public async Task<ActionResult<HeartBeatResult>> GetHeartBeat()
        {
            return await mediatr.Send(new HeartbeatQuery());
        }

        /// <summary>
        /// Get Application Version
        /// </summary>
        [HttpGet("version")]
        public async Task<ActionResult<AppVersionResult>> GetVersion()
        {
            return await mediatr.Send(new AppVersionQuery());
        }

        /// <summary>
        /// See how exception handling works
        /// </summary>
        [HttpGet("test-exception/{msg}")]
        public async Task<ActionResult<HeartBeatResult>> TestException(string msg)
        {
            throw new Exception($"test exception: {msg}");
        }

        /// <summary>
        /// See how exception handling works from within the mediatr pipeline
        /// </summary>
        [HttpGet("test-pipeline-exception/{msg}")]
        public async Task<ActionResult<string>> TestPipelineException(string msg)
        {
            return await mediatr.Send(new TestExceptionQuery{Msg = msg});
        }

    }
}