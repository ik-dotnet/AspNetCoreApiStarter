using System;
using CodeStresmAspNetCoreApiStarter.Infrastructure;

namespace CodeStresmAspNetCoreApiStarter.ViewModels
{
    public class HeartBeatViewModel
    {
        public string Version { get; set; }
        public string Environment { get; set; }
        public string Name { get; set; }
        public DateTime UtcTimestamp { get; set; } = DateTime.UtcNow;
        public string swaggerUrl => "/swagger";
        public string appVersionUrl => "/version";
    }
}