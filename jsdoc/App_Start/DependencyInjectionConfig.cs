using jsdoc.Injection;
using jsdoc.Writer;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace jsdoc
{
    public static class DependencyInjectionConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IReader, Reader>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);


        }
    }
}