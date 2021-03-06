# LightInject.MicrosoftDI
Implementation of Microsoft IServiceCollection, IServiceProvider based on LightInject Container

# Sample AspNetCore

```
public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Create Container wrapper over IServiceCollection
            var container = new ServiceContainer(services);
            container.RegisterInstance<IServiceContainer>(container);

            /**
              Container customization
            /**/

            // Apply all services registered through the IServiceCollection and return IServiceProvider
            return container.CreateServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {  
            /**
            Starting app
            /**/
            
            app.UseMvc();
        }
    }
```

For the force properties injection just add `PropertyInjectionAttribute`:
```
    [PropertyInjection]
    public abstract class BaseController : Controller
    {
        public IMyService Service { get; set; }
    }
```
