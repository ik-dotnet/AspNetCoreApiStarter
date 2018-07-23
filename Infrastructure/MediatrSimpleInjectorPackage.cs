using System;
using CodeStresmAspNetCoreApiStarter.Infrastructure.MediatR;
using MediatR;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace CodeStresmAspNetCoreApiStarter.Infrastructure
{
    public class MediatrSimpleInjectorPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Collection.Register(typeof(INotificationHandler<>), assemblies);
            container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            {
                typeof(ErrorHandlerMediatrPipeline<,>),
                typeof(LogDNAMediatrPipeline<,>)
            });

            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);

        }
    }
}