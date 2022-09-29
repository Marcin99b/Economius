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
            var types = assembly
                .GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass);
            var controllers = types
                .Where(x => x.IsAssignableTo(typeof(IController)))
                .ToArray();
            var viewsServices = types
                .Where(x => x.IsAssignableTo(typeof(IViewsService)))
                .ToArray();
            var botCommands = types
                .Where(x => x.IsAssignableTo(typeof(IBotCommand)))
                .ToArray();
            
            WorkflowTypesContainer.Inject(container, controllers, viewsServices, botCommands);

            var onEventActions = types
                .Where(x => x.IsAssignableTo(typeof(IOnEventAction)))
                .ToArray();

            var eventRunners = onEventActions
                .Select(x => (IOnEventAction)container.Resolve(x))
                .OrderBy(x => (int)x.Order);
            foreach (var eventRunner in eventRunners)
            {
                runner.ConfigureClient(x => eventRunner.Configure(x));
            }

            return runner.Run(token);
        }
    }
}
