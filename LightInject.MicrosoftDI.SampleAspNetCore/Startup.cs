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
            var container = ServiceContainer.Create(services);
            container.RegisterInstance<IServiceContainer>(container);

            AppStarter.Start(container);

            return container.CreateServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
