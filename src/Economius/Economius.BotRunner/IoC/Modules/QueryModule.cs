using Autofac;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Economius.Cqrs;

namespace Economius.BotRunner.IoC.Modules
{
    [ExcludeFromCodeCoverage]
    public class QueryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var list = new List<string>();
            var stack = new Stack<Assembly>();

            var entryAssembly = Assembly.GetEntryAssembly()!;
            if (entryAssembly.FullName!.ToLower().Contains("testhost"))
            {
                var botDllPath = entryAssembly.Location.Replace("testhost.dll", "Economius.BotRunner.dll");
                entryAssembly = Assembly.LoadFrom(botDllPath);
            }
            stack.Push(entryAssembly);
            do
            {
                var asm = stack.Pop();

                var handlers = asm.GetTypes()
                    .Where(type => typeof(IQueryHandler).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    .ToArray();

                foreach (var handler in handlers)
                {
                    builder.RegisterType(handler)
                        .As(handler.GetInterfaces().First())
                        .PreserveExistingDefaults()
                        .SingleInstance();
                }

                foreach (var reference in asm.GetReferencedAssemblies())
                {
                    if (!reference.FullName.Contains("Economius") && !reference.FullName.Contains("Economius"))
                    {
                        continue;
                    }

                    if (!list.Contains(reference.FullName))
                    {
                        stack.Push(Assembly.Load(reference));
                        list.Add(reference.FullName);
                    }
                }
            }
            while (stack.Count > 0);

            builder.RegisterType<QueryBus>()
                .As<IQueryBus>()
                .SingleInstance();
        }
    }
}