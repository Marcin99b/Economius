using Autofac;
using Discord.Net;
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
            return runner.Run(token);
        }
    }
}
