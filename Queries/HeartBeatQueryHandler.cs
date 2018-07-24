using System.Threading;
using System.Threading.Tasks;
using CodeStresmAspNetCoreApiStarter.ViewModels;
using MediatR;

namespace CodeStresmAspNetCoreApiStarter.Queries
{
    public class HeartBeatQueryHandler : IRequestHandler<HeartBeatQuery, HeartBeatViewModel>
    {
        private readonly IMediator mediatr;
        private readonly AppSettings appSettings;

        public HeartBeatQueryHandler(IMediator mediatr, AppSettings appSettings)
        {
            this.mediatr = mediatr;
            this.appSettings = appSettings;
        }

        public async Task<HeartBeatViewModel> Handle(HeartBeatQuery qry, CancellationToken cancellationToken)
        {
            var appVersion = await mediatr.Send(new AppVersionQuery { CorrelationId = qry.CorrelationId }, cancellationToken);

            return new HeartBeatViewModel
            {
                Version = appVersion.Version,
                Name = typeof(Program).Assembly.FullName, 
                Environment = appSettings.Environment
            };
        }
    }
}