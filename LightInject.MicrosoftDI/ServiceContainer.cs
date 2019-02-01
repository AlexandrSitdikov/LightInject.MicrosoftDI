namespace LightInject.MicrosoftDI
{
    using System;

    using LightInject.MicrosoftDI.Internal;

    using Microsoft.Extensions.DependencyInjection;

    public partial class ServiceContainer : LightInject.ServiceContainer, IServiceContainer
    {
        [ThreadStatic]
        private static VarianceFilter varianceFilterConfig;

        private IServiceCollection serviceDescriptors;
        private VarianceFilter varianceFilter;

        public ServiceContainer(IServiceCollection serviceDescriptors, ContainerOptions options = null) : base(ApplyOptions(options))
        {
            this.varianceFilter = varianceFilterConfig;
            this.serviceDescriptors = serviceDescriptors ?? throw new ArgumentNullException(nameof(serviceDescriptors));

            this.PropertyDependencySelector = new PropertyDependencySelector(this.PropertyDependencySelector);
            this.RegisterInstance<IServiceCollection>(this);
        }

        public IServiceContainer CreateServiceProvider()
        {
            return this.RegisterServices(this.serviceDescriptors);
        }

        public object GetService(Type serviceType)
        {
            return this.GetInstance(serviceType);
        }

        private static ContainerOptions ApplyOptions(ContainerOptions options)
        {
            if (options == null)
            {
                options = new ContainerOptions();
            }

            varianceFilterConfig = new VarianceFilter(options.VarianceFilter);
            options.VarianceFilter = varianceFilterConfig.Filter;

            return options;
        }
    }
}