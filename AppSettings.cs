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
        public string CorsAllowedOrigins => config.GetValue<string>("Cors:AllowedOrigins");
        public string LogDNAApp => config.GetValue<string>("LogDNA:App");
        public string LogDNAHostname => config.GetValue<string>("LogDNA:Hostname");
        public string LogDNAInjestionKey => config.GetValue<string>("LogDNA:InjestionKey");
        public string Environment => config.GetValue<string>("Environment");
    }
}