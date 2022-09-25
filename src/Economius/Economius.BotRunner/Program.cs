using Discord;
using Discord.Net;
using Discord.WebSocket;
using Economius.BotRunner.IoC;
using Newtonsoft.Json;

namespace Economius.BotRunner
{
    public class Program
    {
        //todo ioc
        private readonly CommandsConfig commandsConfig;
        private readonly CommandsRouter commandsRouter;
        private readonly DiscordSocketClient client;

        public Program()
        {
            this.commandsConfig = new CommandsConfig();
            this.commandsRouter = new CommandsRouter();
            this.client = new DiscordSocketClient();
        }

        public static Task Main(string[] args) => new Program().MainAsync(args);

        public async Task MainAsync(string[] args)
        {
            var token = Environment.GetEnvironmentVariable("discord_token")!;
            var mongoConnectionString = Environment.GetEnvironmentVariable("economius_mongodb")!;
            var container = new ContainerModule(mongoConnectionString)
                .GetBuilder()
                .Build();

            this.client.Log += this.Log;

            await this.client.LoginAsync(TokenType.Bot, token);
            await this.client.StartAsync();

            this.client.Ready += this.Client_Ready;
            this.client.SlashCommandExecuted += this.SlashCommandHandler;

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task Client_Ready()
        {
            await this.client.BulkOverwriteGlobalApplicationCommandsAsync(this.commandsConfig.CommandsInfos);
        }

        private Task SlashCommandHandler(SocketSlashCommand command)
        {
            //todo there may be performance issue
            var json = JsonConvert.SerializeObject(command.Data, Formatting.Indented);
            Console.WriteLine(json);
            return this.commandsRouter.Route(command);
        }
    }
}
