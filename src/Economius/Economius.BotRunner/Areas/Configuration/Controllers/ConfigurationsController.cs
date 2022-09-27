using Discord;
using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using Economius.BotRunner.Areas.Configuration.Commands;
using Economius.BotRunner.Areas.Configuration.Views;
using Economius.Cqrs;
using Economius.Domain.Configuration;
using Economius.Domain.Configuration.Cqrs;

namespace Economius.BotRunner.Areas.Configuration.Controllers
{
    public interface IConfigurationsController
    {
        Task<IViewModel> SetupServer(SocketSlashCommand rawCommand, SetupServerCommand setupServerCommand);
        Task<IViewModel> ShowServerSetup(SocketSlashCommand rawCommand, ShowServerSetupCommand command);
    }

    public class ConfigurationsController : IConfigurationsController
    {
        private readonly IQueryBus queryBus;
        private readonly ICommandBus commandBus;

        public ConfigurationsController(IQueryBus queryBus, ICommandBus commandBus)
        {
            this.queryBus = queryBus;
            this.commandBus = commandBus;
        }

        public async Task<IViewModel> SetupServer(SocketSlashCommand rawCommand, SetupServerCommand setupServerCommand)
        {
            var command = new AddOrUpdateServerConfigurationCommand(
                rawCommand.GuildId!.Value,
                setupServerCommand.UserStartMoney,
                setupServerCommand.ServerStartMoney,
                setupServerCommand.IncomeTaxPercentage);
            await this.commandBus.ExecuteAsync(command);

            var result = await this.PrintServerConfiguration(rawCommand);
            return result;
        }

        public async Task<IViewModel> ShowServerSetup(SocketSlashCommand rawCommand, ShowServerSetupCommand command)
        {
            var result = await this.PrintServerConfiguration(rawCommand);
            return result;
        }

        private async Task<IViewModel> PrintServerConfiguration(SocketSlashCommand rawCommand)
        {
            var serverId = rawCommand.GuildId!.Value;
            var query = new GetServerConfigurationQuery(serverId);
            var result = await this.queryBus.ExecuteAsync(query);
            var serverConfiguration = result.ServerConfiguration;

            if (serverConfiguration == null)
            {
                await rawCommand.RespondAsync("Server is not configured");
                return new EmptyViewModel();
            }

            return new ShowServerSetupViewModel(
                serverConfiguration.UserStartMoney,
                serverConfiguration.ServerStartMoney,
                serverConfiguration.IncomeTaxPercentage);
        }

    }
}
