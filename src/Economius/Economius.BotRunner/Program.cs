using Autofac;
using Autofac.Util;
using Discord.Net;
using Economius.BotRunner.Areas.Commons;
using Economius.BotRunner.Areas.Payments.OnEventActions;
using Economius.BotRunner.IoC;
using Economius.Infrastructure.Database.MongoDB;
using System.Reflection;

namespace Economius.BotRunner
{
    public class Program
    {
        public static Task Main()
        {
            var token = Environment.GetEnvironmentVariable("discord_token")!;
            var mongoConnectionString = Environment.GetEnvironmentVariable("economius_mongodb")!;
            var container = new ContainerModule(mongoConnectionString)
                .GetBuilder()
                .Build();

            MongoConfiguration.Initialize();

            var runner = container.Resolve<IEconomiusRunner>();

            var assembly = Assembly.GetEntryAssembly()!;
            var loadableTypes = assembly
                .GetLoadableTypes()
                .Where(x => !x.IsAbstract && x.IsClass);
            var controllers = loadableTypes
                .Where(x => x.IsAssignableTo(typeof(IController)))
                .ToArray();
            var viewsServices = loadableTypes
                .Where(x => x.IsAssignableTo(typeof(IViewsService)))
                .ToArray();

            WorkflowTypesContainer.Inject(container, controllers, viewsServices);

            var onEventActions = loadableTypes
                .Where(x => x.IsAssignableTo(typeof(IOnEventAction)))
                .ToArray();
            foreach (var onEventActionType in onEventActions)
            {
                var instance = (IOnEventAction)container.Resolve(onEventActionType);
                runner.ConfigureClient(x => instance.Configure(x));
            }

            return runner.Run(token);
        }
    }
}
