using System;
using Autofac;

namespace MVC_Core
{
    public class ServiceProvider : IServiceProvider
    {
        protected IContainer container;

        public ServiceProvider(IContainer container)
        {
            this.container = container;
        }

        public void Dispose()
        {
            IDisposable disposable = (IDisposable)container;
            if (disposable != null)
            {
                disposable.Dispose();
            }
            container = null;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
                throw new Exception(
                    string.Format("Please implement the interface: {0}", serviceType.Name));

            if (serviceType.IsInterface)
                return container.Resolve(serviceType);

            return container.Resolve(serviceType);
        }
    }
}
