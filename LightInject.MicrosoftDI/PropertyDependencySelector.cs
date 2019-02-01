namespace LightInject.MicrosoftDI
{
    using System;
    using System.Collections.Generic;

    public class PropertyDependencySelector : IPropertyDependencySelector
    {
        private readonly IPropertyDependencySelector innerPropertyDependencySelector;

        private readonly List<Type> ignoredTypes = new List<Type>();
        private readonly List<Type> ignoredGenericTypes = new List<Type>();

        private readonly PropertyDependency[] empty = new PropertyDependency[0];

        public PropertyDependencySelector(IPropertyDependencySelector innerPropertyDependencySelector)
        {
            this.innerPropertyDependencySelector = innerPropertyDependencySelector;
        }

        public IEnumerable<PropertyDependency> Execute(Type type)
        {
            if (type.IsGenericType)
            {
                if (this.ignoredGenericTypes.Contains(type.GetGenericTypeDefinition()))
                {
                    return this.empty;
                }
            }
            else
            {
                if (this.ignoredTypes.Contains(type))
                {
                    return this.empty;
                }
            }

            return this.innerPropertyDependencySelector.Execute(type);
        }

        public void IgnoreType(Type type)
        {
            if (type.IsGenericTypeDefinition)
            {
                if (!this.ignoredGenericTypes.Contains(type))
                {
                    this.ignoredGenericTypes.Add(type);
                }
            }
            else
            {
                if (!this.ignoredTypes.Contains(type))
                {
                    this.ignoredTypes.Add(type);
                }
            }
        }
    }
}