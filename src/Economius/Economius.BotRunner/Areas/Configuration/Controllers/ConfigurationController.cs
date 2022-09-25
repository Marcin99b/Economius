using Discord;
using Discord.WebSocket;
using Economius.BotRunner.Areas.Configuration.Commands;
using Economius.Cqrs;
using Economius.Domain.Configuration;

namespace Economius.BotRunner.Areas.Configuration.Controllers
{
    public interface IConfigurationController
    {
        Task SetupServer(SocketSlashCommand rawCommand, SetupServerCommand command);
        Task ShowServerSetup(SocketSlashCommand rawCommand, ShowServerSetupCommand command);
    }

    public class ConfigurationController : IConfigurationController
    {
        private readonly IQueryBus queryBus;
        private readonly ICommandBus commandBus;

        public ConfigurationController(IQueryBus queryBus, ICommandBus commandBus)
        {
            this.queryBus = queryBus;
            this.commandBus = commandBus;
        }

        public async Task SetupServer(SocketSlashCommand rawCommand, SetupServerCommand setupServerCommand)
        {
            var command = new AddOrUpdateServerConfigurationCommand(
                rawCommand.GuildId!.Value,
                setupServerCommand.UserStartMoney,
                setupServerCommand.ServerStartMoney,
                setupServerCommand.IncomeTaxPercentage);
            await this.commandBus.ExecuteAsync(command);

            await this.PrintServerConfiguration(rawCommand);
        }

        public Task ShowServerSetup(SocketSlashCommand rawCommand, ShowServerSetupCommand command)
        {
            return this.PrintServerConfiguration(rawCommand);
        }
        
        private async Task PrintServerConfiguration(SocketSlashCommand rawCommand)
        {
            var serverId = rawCommand.GuildId!.Value;
            var query = new GetServerConfigurationQuery(serverId);
            var result = await this.queryBus.ExecuteAsync(query);
            var serverConfiguration = result.ServerConfiguration;

            if (serverConfiguration == null)
            {
                await rawCommand.RespondAsync("Server is not configured");
                return;
            }

            var embed = new EmbedBuilder()
                .WithTitle("Server configuration")
                .WithFields(new[] 
                { 
                    new EmbedFieldBuilder()
                        .WithName(SetupServerCommand.Param_UserStartMoney) 
                        .WithValue(serverConfiguration.UserStartMoney),

                    new EmbedFieldBuilder()
                        .WithName(SetupServerCommand.Param_ServerStartMoney)
                        .WithValue(serverConfiguration.ServerStartMoney),

                    new EmbedFieldBuilder()
                        .WithName(SetupServerCommand.Param_IncomeTaxPercentage)
                        .WithValue(serverConfiguration.IncomeTaxPercentage),
                })
                .Build();

            await rawCommand.RespondAsync(embed: embed);
        }
        
    }
}
