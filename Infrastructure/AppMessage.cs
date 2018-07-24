using System;
using System.Collections.Generic;

namespace CodeStresmAspNetCoreApiStarter.Infrastructure
{
    public class AppMessage
    {
        public string CorrelationId { get; set; } = Guid.NewGuid().ToString();
        public string LoggedOnUser { get; set; }
        public List<string> Logs { get; set; } = new List<string>();
    }
}