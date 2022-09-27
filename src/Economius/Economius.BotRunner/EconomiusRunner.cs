using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace Economius.BotRunner
{
    public interface IEconomiusRunner
    {
        Task Run(string token);
        void ConfigureClient(Action<DiscordSocketClient> action);
    }

    public class EconomiusRunner : IEconomiusRunner
    {
        private readonly ICommandsConfig commandsConfig;
        private readonly ICommandsRouter commandsRouter;
        private DiscordSocketClient client = new DiscordSocketClient();

        public EconomiusRunner(ICommandsConfig commandsConfig, ICommandsRouter commandsRouter)
        {
            this.commandsConfig = commandsConfig;
            this.commandsRouter = commandsRouter;
        }

        public async Task Run(string token)
        {
            this.client.Log += this.Log;

            await this.client.LoginAsync(TokenType.Bot, token);
            await this.client.StartAsync();

            this.client.Ready += this.ClientReady;
            this.client.SlashCommandExecuted += this.SlashCommandHandler;


            await Task.Delay(-1);
        }

        public void ConfigureClient(Action<DiscordSocketClient> action)
        {
            action.Invoke(this.client);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task ClientReady()
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
