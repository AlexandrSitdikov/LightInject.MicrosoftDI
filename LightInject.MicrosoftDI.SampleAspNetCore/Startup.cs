namespace LightInject.MicrosoftDI.SampleAspNetCore
{
    using System;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Create Container wrapper over IServiceCollection
            var container = new ServiceContainer(services);
            container.RegisterInstance<IServiceContainer>(container);

            // Configurring container
            AppStarter.Init(container);

            // Apply all services registered through the IServiceCollection and return IServiceProvider
            return container.CreateServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceContainer container)
        {
            // Starting app
            AppStarter.Start(container);

            app.UseMvc();
        }
    }
}
