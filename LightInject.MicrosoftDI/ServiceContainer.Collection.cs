namespace LightInject.MicrosoftDI
{
    using System.Collections;
    using System.Collections.Generic;

    using Microsoft.Extensions.DependencyInjection;

    public partial class ServiceContainer : IServiceCollection
    {
        public ServiceDescriptor this[int index] { get => this.serviceDescriptors[index]; set => this.serviceDescriptors[index] = value; }

        public int Count => this.serviceDescriptors.Count;

        public bool IsReadOnly => this.serviceDescriptors.IsReadOnly;

        public void Add(ServiceDescriptor item)
        {
            this.serviceDescriptors.Add(item);
        }

        public void Clear()
        {
            this.serviceDescriptors.Clear();
        }

        public bool Contains(ServiceDescriptor item)
        {
            return this.serviceDescriptors.Contains(item);
        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            this.serviceDescriptors.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return this.serviceDescriptors.GetEnumerator();
        }

        public int IndexOf(ServiceDescriptor item)
        {
            return this.serviceDescriptors.IndexOf(item);
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            this.serviceDescriptors.Insert(index, item);
        }

        public bool Remove(ServiceDescriptor item)
        {
            return this.serviceDescriptors.Remove(item);
        }

        public void RemoveAt(int index)
        {
            this.serviceDescriptors.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}