using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI
{
    /// <summary>
    /// Helper methods for the IoC container.
    /// </summary>
    public static class ServiceContainerExtensions
    {
        public static void AddService<T>(this IServiceContainer container, Func<IServiceProvider, T> factory)
        {
            container.AddService(typeof(T), (c, type) => factory(c));
        }

        public static void AddType<TService, TConcrete>(this IServiceContainer container)
        {
            container.AddService(typeof(TService),
               (c, type) =>
               {
                   object[] args = ResolveConstructorArguments<TConcrete>(c);
                   return Activator.CreateInstance(typeof(TConcrete), args);
               });
        }

        public static void RemoveType<TService>(this IServiceContainer container)
        {
            container.RemoveService(typeof(TService));
        }

        public static void AddType<TConcrete>(this IServiceContainer container)
        {
            container.AddService(typeof(TConcrete),
               (c, type) =>
               {
                   object[] args = ResolveConstructorArguments<TConcrete>(c);
                   return Activator.CreateInstance(typeof(TConcrete), args);
               });
        }

        private static object[] ResolveConstructorArguments<T>(IServiceContainer container)
        {
            var constructor = typeof(T).GetConstructors().First();

            var arguments = new List<object>();
            var parameters = constructor.GetParameters();
            foreach (var parameter in parameters)
            {
                arguments.Add(container.GetService(parameter.ParameterType));
            }

            return arguments.ToArray();
        }

        public static T GetService<T>(this IServiceContainer container)
        {
            return (T)container.GetService(typeof(T));
        }
    }
}
