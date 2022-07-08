namespace LightInject.MicrosoftDI
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    using LightInject.MicrosoftDI.Internal;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public partial class ServiceContainer
    {
        private int registrationIndex;

        protected IServiceContainer RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Replace(ServiceDescriptor.Singleton(typeof(IServiceScopeFactory), new ServiceScopeFactory(this)));
            serviceCollection.Replace(ServiceDescriptor.Singleton(typeof(IServiceCollection), this));
            serviceCollection.Replace(ServiceDescriptor.Singleton(typeof(IServiceProvider), this));

            if (this.PropertyDependencySelector is PropertyDependencySelector propertyDependencySelector)
            {
                foreach (var descriptor in serviceCollection)
                {
                    if (descriptor.ImplementationType != null)
                    {
                        propertyDependencySelector.IgnoreType(descriptor.ImplementationType);
                    }
                }
            }

            foreach (var groupedRegistrations in serviceCollection.GroupBy(x => x.ServiceType))
            {
                var registration = this.CreateServiceRegistration(groupedRegistrations.Last());

                registration.ServiceName = this.registrationIndex.ToString("D8", CultureInfo.InvariantCulture.NumberFormat);
                this.Register(registration);
                this.registrationIndex++;

                this.varianceFilter.IgnoreType(registration.ServiceType);

                if (groupedRegistrations.Count() > 1)
                {
                    this.Register(new ServiceRegistration
                    {
                        FactoryExpression = (Delegate)this.GetType()
                            .GetMethod(nameof(MultipleServicesFactory), BindingFlags.Instance | BindingFlags.NonPublic)
                            .MakeGenericMethod(registration.ServiceType)
                            .Invoke(this, new object[] { groupedRegistrations.ToArray() }),
                        Lifetime = registration.Lifetime,
                        ServiceName = registration.ServiceType.Name + "_multireg",
                        ServiceType = typeof(IEnumerable<>).MakeGenericType(registration.ServiceType)
                    });
                }
            }

            return this;
        }

        protected ServiceRegistration CreateServiceRegistration(ServiceDescriptor serviceDescriptor)
        {
            if (serviceDescriptor.ImplementationFactory != null)
            {
                return CreateServiceRegistrationForFactoryDelegate(serviceDescriptor, serviceDescriptor.ImplementationFactory);
            }

            if (serviceDescriptor.ImplementationInstance != null)
            {
                return CreateServiceRegistrationForInstance(serviceDescriptor);
            }

            return CreateServiceRegistrationServiceType(serviceDescriptor);
        }

        protected ServiceRegistration CreateServiceRegistrationServiceType(ServiceDescriptor serviceDescriptor)
        {
            ServiceRegistration registration = CreateBasicServiceRegistration(serviceDescriptor);
            registration.ImplementingType = serviceDescriptor.ImplementationType;
            return registration;
        }

        protected ServiceRegistration CreateServiceRegistrationForInstance(ServiceDescriptor serviceDescriptor)
        {
            ServiceRegistration registration = CreateBasicServiceRegistration(serviceDescriptor);
            registration.Value = serviceDescriptor.ImplementationInstance;
            return registration;
        }

        protected ServiceRegistration CreateServiceRegistrationForFactoryDelegate(ServiceDescriptor serviceDescriptor, Delegate factory)
        {
            ServiceRegistration registration = CreateBasicServiceRegistration(serviceDescriptor);
            registration.FactoryExpression = factory;
            return registration;
        }

        protected ServiceRegistration CreateBasicServiceRegistration(ServiceDescriptor serviceDescriptor)
        {
            return new ServiceRegistration
            {
                Lifetime = ResolveLifetime(serviceDescriptor),
                ServiceType = serviceDescriptor.ServiceType,
                ServiceName = Guid.NewGuid().ToString()
            };
        }

        protected virtual ILifetime ResolveLifetime(ServiceDescriptor serviceDescriptor)
        {
            if (serviceDescriptor.ImplementationInstance != null)
            {
                return null;
            }

            ILifetime lifetime = null;

            switch (serviceDescriptor.Lifetime)
            {
                case ServiceLifetime.Scoped:
                    lifetime = new PerScopeLifetime();
                    break;
                case ServiceLifetime.Singleton:
                    lifetime = new PerContainerLifetime();
                    break;
                case ServiceLifetime.Transient:
                    lifetime = new PerRequestLifeTime();
                    break;
            }

            return lifetime;
        }

        protected virtual Func<IServiceProvider, IEnumerable<T>> MultipleServicesFactory<T>(IEnumerable<ServiceDescriptor> serviceDescriptors)
        {
            return new Func<IServiceProvider, IEnumerable<T>>(x =>
            serviceDescriptors.Select(r => (T)(r.ImplementationInstance ?? (r.ImplementationType == null ? null : this.Create(r.ImplementationType)) ?? r.ImplementationFactory(x))));
        }
    }
}