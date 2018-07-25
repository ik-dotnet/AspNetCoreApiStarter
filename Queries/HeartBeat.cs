using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CodeStresmAspNetCoreApiStarter.Infrastructure;
using MediatR;

namespace CodeStresmAspNetCoreApiStarter.ViewModels
{
    public class HeartBeat
    {
        public string Version { get; set; }
        public string Environment { get; set; }
        public string Name { get; set; }
        public DateTime UtcTimestamp { get; set; } = DateTime.UtcNow;
        public string swaggerUrl => "/swagger";
        public string appVersionUrl => "/version";

        public class Query : AppMessage, IRequest<HeartBeat> { }

        public class Handler : IRequestHandler<Query, HeartBeat>
        {
            private readonly IMediator mediatr;
            private readonly AppSettings appSettings;
            private readonly IMapper mapper;

            public Handler(IMediator mediatr, AppSettings appSettings, IMapper mapper)
            {
                this.mediatr = mediatr;
                this.appSettings = appSettings;
                this.mapper = mapper;
            }

            public async Task<HeartBeat> Handle(Query qry, CancellationToken cancellationToken)
            {
                var appVersion = await mediatr.Send(mapper.Map<AppVersion.Query>(qry), cancellationToken);

                return new HeartBeat
                {
                    Version = appVersion.Version,
                    Name = typeof(Program).Assembly.FullName,
                    Environment = appSettings.Environment
                };
            }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Query, AppVersion.Query>();
            }
        }
    }
}