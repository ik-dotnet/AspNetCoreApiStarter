using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace CodeStresmAspNetCoreApiStarter.Controllers
{
    public class UtilityController
    {
        [HttpGet]
        [Route("api/version")]
        public object GetVersion()
        {
            var version = typeof(Program).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
            return new { version };
        }

    }
}