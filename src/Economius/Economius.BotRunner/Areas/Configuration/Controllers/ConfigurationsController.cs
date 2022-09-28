using Discord;
using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using Economius.BotRunner.Areas.Configuration.Commands;
using Economius.BotRunner.Areas.Configuration.Views;
using Economius.Cqrs;
using Economius.Domain.Configurations;
using Economius.Domain.Configurations.Cqrs;

namespace Economius.BotRunner.Areas.Configuration.Controllers
{
    public interface IConfigurationsController : IController
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
                setupServerCommand.IncomeTaxPercentage);
            await this.commandBus.ExecuteAsync(command);

            var result = this.PrintServerConfiguration(rawCommand);
            return result;
        }

        public Task<IViewModel> ShowServerSetup(SocketSlashCommand rawCommand, ShowServerSetupCommand command)
        {
            var result = this.PrintServerConfiguration(rawCommand);
            return Task.FromResult(result);
        }

        private IViewModel PrintServerConfiguration(SocketSlashCommand rawCommand)
        {
            var serverId = rawCommand.GuildId!.Value;
            var query = new GetServerConfigurationQuery(serverId);
            var serverConfiguration = this.queryBus.Execute(query).ServerConfiguration;

            if (serverConfiguration == null)
            {
                return new EmptyViewModel(message: "Server is not configured");
            }

            return new ShowServerSetupViewModel(
                serverConfiguration.IncomeTaxPercentage);
        }
    }
}
