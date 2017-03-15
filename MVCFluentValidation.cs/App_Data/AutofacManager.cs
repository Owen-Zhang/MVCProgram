using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;

namespace MVC_Core
{
    public class AutofacManager
    {
        static AutofacManager manager;
        static IContainer container;

        AutofacManager()
        {
        }

        public static AutofacManager Current
        {
            get
            {
                if (manager == null)
                    manager = new AutofacManager();
                return manager;
            }
        }

        public static T GetService<T>()
        {
            return Current.Container.Resolve<T>();
        }

        public IContainer Container
        {
            get
            {
                if (container == null)
                    Init();
                return container;
            }
        }

        public void Init()
        {
            var assemblyStrList = new List<string>{
                "DoMainService.dll",
                "DoMainService.Imp.dll",
                "DataService.dll",
                "DataService.Imp.dll"
            };

            List<Assembly> assemblies = new List<Assembly>();
            foreach (var item in assemblyStrList)
            {
                assemblies.Add(
                    Assembly.LoadFile(Path.Combine(HttpContext.Current.Server.MapPath("~"), "bin", item)));
            }

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly()); //Assembly.LoadFile(Path.Combine(HttpContext.Current.Server.MapPath("~"), "bin", "MVCFluentValidation.cs.dll")));

            builder.RegisterAssemblyTypes(assemblies.ToArray())
                    .Where(type => typeof(IDependency).IsAssignableFrom(type) && !type.IsAbstract)
                    .AsImplementedInterfaces().InstancePerLifetimeScope();
            container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //new ServiceProvider(container);
        }
    }
}
