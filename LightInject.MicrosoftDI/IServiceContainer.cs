namespace LightInject.MicrosoftDI
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    public interface IServiceContainer : LightInject.IServiceContainer, IServiceCollection, IServiceProvider
    {
        IServiceContainer CreateServiceProvider();
    }
}