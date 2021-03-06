﻿namespace LightInject.MicrosoftDI.SampleAspNetCore
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public class AppStarter
    {
        public static void Init(IServiceContainer container)
        {
            container.Register<IModule, Module1>(nameof(Module1));
            container.Register<IModule, Module2>(nameof(Module2));

            foreach (var module in container.GetAllInstances<IModule>())
            {
                module.Init();
            }
        }

        public static void Start(IServiceContainer container)
        {
            var cls1 = container.GetInstance<AppStarter.Class1>();

            if (cls1.ContructableClass2 != null &&
                cls1.ContructableClass2.PropertyClass3 != null &&
                cls1.ContructableClass2.ContructableClass3 != null &&
                cls1.PropertyClass2 != null &&
                cls1.PropertyClass2.PropertyClass3 != null &&
                cls1.PropertyClass2.ContructableClass3 != null)
            {
                Console.WriteLine("Container is valid");
            }
            else
            {
                throw new ApplicationException();
            }
        }

        private interface IModule
        {
            void Init();
        }

        private class Module1 : IModule
        {
            public IServiceContainer Container { get; set; }

            public void Init()
            {
                this.Container.AddMvc()
                    .AddControllersAsServices()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
                this.Container.Register<Class1>();
            }
        }

        private class Module2 : IModule
        {
            public IServiceContainer container;

            public Module2(IServiceContainer container)
            {
                this.container = container;
            }

            public void Init()
            {
                this.container.Register<Class2>();
                this.container.Register<Class3>();
            }
        }

        public class Class1
        {
            public Class1(Class2 contructableClass2)
            {
                ContructableClass2 = contructableClass2;
            }

            public Class2 PropertyClass2 { get; set; }

            public Class2 ContructableClass2 { get; }
        }

        public class Class2
        {
            public Class2(Class3 contructableClass3)
            {
                ContructableClass3 = contructableClass3;
            }

            public Class3 PropertyClass3 { get; set; }

            public Class3 ContructableClass3 { get; }
        }

        public class Class3
        {

        }
    }
}