namespace LightInject.MicrosoftDI.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class VarianceFilter
    {
        private List<Type> ignoredTypes = new List<Type>();
        private Func<Type, bool> innerFilterAction = x => true;

        public VarianceFilter(Func<Type, bool> innerFilterAction)
        {
            if (innerFilterAction != null)
            {
                this.innerFilterAction = innerFilterAction;
            }
        }

        public void IgnoreType(Type type)
        {
            this.ignoredTypes.Add(type);
        }

        public bool Filter(Type type)
        {
            if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IEnumerable<>))
            {
                type = type.GetInterfaces().First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            }

            return !this.ignoredTypes.Contains(type.GetGenericArguments()[0]) && this.innerFilterAction(type);
        }
    }
}