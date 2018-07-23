using CodeStresmAspNetCoreApiStarter.Data;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace CodeStresmAspNetCoreApiStarter.Infrastructure
{
    public class AppSimpleInjectorPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.RegisterSingleton<AppSettings>();
            container.Register<DapperContext>(Lifestyle.Scoped);

            //container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            //{
            //    typeof(ErrorHandlerMediatrPipeline<,>),
            //    typeof(LogDNAMediatrPipeline<,>)
            //});
        }
    }
}