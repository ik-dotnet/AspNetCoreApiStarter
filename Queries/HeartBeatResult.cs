using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CodeStresmAspNetCoreApiStarter.Infrastructure;
using MediatR;

namespace CodeStresmAspNetCoreApiStarter.ViewModels
{
    public class HeartBeatResult
    {
        public string Version { get; set; }
        public string Environment { get; set; }
        public string Name { get; set; }
        public DateTime UtcTimestamp { get; set; } = DateTime.UtcNow;
        public string swaggerUrl => "/swagger";
        public string appVersionUrl => "/version";
    }

    public class HeartbeatQuery : AppMessage, IRequest<HeartBeatResult> { }

    public class HeartbeatQueryHandler : IRequestHandler<HeartbeatQuery, HeartBeatResult>
    {
        private readonly IMediator mediatr;
        private readonly AppSettings appSettings;
        private readonly IMapper mapper;

        public HeartbeatQueryHandler(IMediator mediatr, AppSettings appSettings, IMapper mapper)
        {
            this.mediatr = mediatr;
            this.appSettings = appSettings;
            this.mapper = mapper;
        }

        public async Task<HeartBeatResult> Handle(HeartbeatQuery qry, CancellationToken cancellationToken)
        {
            var appVersion = await mediatr.Send(mapper.Map<AppVersionQuery>(qry), cancellationToken);

            return new HeartBeatResult
            {
                Version = appVersion.Version,
                Name = typeof(Program).Assembly.FullName,
                Environment = appSettings.Environment
            };
        }
    }

    public class HeartbeatMappingProfile : Profile
    {
        public HeartbeatMappingProfile()
        {
            CreateMap<HeartbeatQuery, AppVersionQuery>();
        }
    }

}