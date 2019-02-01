namespace LightInject.MicrosoftDI
{
    using System;

    using LightInject.MicrosoftDI.Internal;

    using Microsoft.Extensions.DependencyInjection;

    public partial class ServiceContainer : LightInject.ServiceContainer, IServiceContainer
    {
        private IServiceCollection serviceDescriptors;
        private VarianceFilter varianceFilter;

        private ServiceContainer(IServiceCollection serviceDescriptors, VarianceFilter varianceFilter, ContainerOptions options) : base(options)
        {
            this.varianceFilter = varianceFilter;
            this.serviceDescriptors = serviceDescriptors ?? throw new ArgumentNullException(nameof(serviceDescriptors));

            this.RegisterInstance<IServiceCollection>(this);

            this.PropertyDependencySelector = new PropertyDependencySelector(this.PropertyDependencySelector);
        }

        public static ServiceContainer Create(IServiceCollection serviceDescriptors, ContainerOptions options = null)
        {
            if (options == null)
            {
                options = new ContainerOptions();
            }

            ServiceContainer container = null;
            var varianceFilter = new VarianceFilter(options.VarianceFilter);

            options.VarianceFilter = varianceFilter.Filter;

            return container = new ServiceContainer(serviceDescriptors, varianceFilter, options);
        }

        public IServiceContainer CreateServiceProvider()
        {
            return this.RegisterServices(this.serviceDescriptors);
        }

        public object GetService(Type serviceType)
        {
            return this.GetInstance(serviceType);
        }
    }
}