using Microsoft.AspNetCore.Mvc;

namespace CodeStresmAspNetCoreApiStarter.Infrastructure
{
    public static class Extensions
    {
        public static ActionResult<T> AsActionResult<T>(this T input)
        {
            return new ActionResult<T>(input);
        }
    }
}