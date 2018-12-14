using AutoMapper;
using System;
using System.IO;
using System.Reflection;
using CodeStream.logDNA;
using CodeStresmAspNetCoreApiStarter.Data;
using CodeStresmAspNetCoreApiStarter.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedBear.LogDNA;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using Swashbuckle.AspNetCore.Swagger;

namespace CodeStresmAspNetCoreApiStarter
{
    public class Startup
    {
        private readonly Container container = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private AppSettings AppSettings => new AppSettings(Configuration);

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var thisAssembly = GetType().Assembly;

            services.AddCors();
            services.AddAutoMapper(thisAssembly);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            AddSimpleInjector(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "CodeStream API", Version = "v1" });
                
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            UseSimpleInjector(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors(builder => builder.WithOrigins(AppSettings.CorsAllowedOrigins)
                .AllowAnyMethod()
                .AllowAnyHeader());

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeStream API V1");
            });

            // global exception handler
            app.UseExceptionHandler(GlobalExceptionHandler.Handler(container.GetInstance<ILogDNALogger>()));
            
            app.UseMvc();
        }

        private void AddSimpleInjector(IServiceCollection services)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(container));
            services.AddSingleton(Configuration);

            //Ref: https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/implement-resilient-entity-framework-core-sql-connections
            services.AddDbContext<EFContext>(opts =>
                opts.UseSqlServer(AppSettings.PrimaryConnectionString,
                sqlServerOptionsAction:  optsAction =>
                {
                    optsAction.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(20), errorNumbersToAdd: null);
                })
            );

            services.EnableSimpleInjectorCrossWiring(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        private void UseSimpleInjector(IApplicationBuilder app)
        {
            container.RegisterMvcControllers(app);
            container.RegisterPackages(AppDomain.CurrentDomain.GetAssemblies());

            // Allow Simple Injector to resolve services from ASP.NET Core.
            container.AutoCrossWireAspNetComponents(app);

            RegisterLogDNAServicesInSimpleInjector(container);

            container.Verify();
        }

        public void RegisterLogDNAServicesInSimpleInjector(Container container)
        {
            //TODO: extract this code into SimpleInjector IPackage class but then we need to solve global static access to AppSettings.

            container.RegisterSingleton(() =>
            {
                var config = new ConfigurationManager(AppSettings.LogDNAInjestionKey)
                {
                    HostName = AppSettings.LogDNAHostname,
                    FlushInterval = 500,
                    Tags = new[] { AppSettings.Environment, Assembly.GetExecutingAssembly().GetName().Version.ToString() }
                };
                var client = config.Initialise();

                client.Connect();

                return client;
            });
            container.RegisterSingleton(() => (ILogDNALogger)new LogDNALogger(container.GetInstance<IApiClient>(), AppSettings.LogDNAApp));
        }


    }
}
