using Autofac;
using Discord.Net;
using Economius.BotRunner.Areas.Payments.OnEventActions;
using Economius.BotRunner.IoC;
using Economius.Infrastructure.Database.MongoDB;

namespace Economius.BotRunner
{
    public class Program
    {
        public static Task Main(string[] args) => new Program().MainAsync(args);

        public Task MainAsync(string[] args)
        {
            var token = Environment.GetEnvironmentVariable("discord_token")!;
            var mongoConnectionString = Environment.GetEnvironmentVariable("economius_mongodb")!;
            var container = new ContainerModule(mongoConnectionString)
                .GetBuilder()
                .Build();

            MongoConfiguration.Initialize();

            var runner = container.Resolve<IEconomiusRunner>();

            //todo reflection
            var createWalletsOnEventAction = container.Resolve<ICreateWalletsOnEventAction>();
            runner.ConfigureClient(x => createWalletsOnEventAction.Configure(x));

            return runner.Run(token);
        }
    }
}
