using System;
using CodeStresmAspNetCoreApiStarter.Data;
using FluentValidation;
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

            RegisterFluentValidators(container);
        }

        private void RegisterFluentValidators(Container container)
        {
            container.Collection.Register(typeof(IValidator<>), AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}