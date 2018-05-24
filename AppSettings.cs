using Microsoft.Extensions.Configuration;

namespace CodeStresmAspNetCoreApiStarter
{
    public class AppSettings
    {
        private readonly IConfiguration config;

        public AppSettings(IConfiguration config)
        {
            this.config = config;
        }

        public string PrimaryConnectionString => config.GetValue<string>("ConnectionStrings:Primary");
    }
}