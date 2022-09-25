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

        public Task SetupServer(SocketSlashCommand rawCommand, SetupServerCommand command)
        {
            var serverConfiguration = new ServerConfiguration(rawCommand.GuildId!.Value, command.UserStartMoney, command.ServerStartMoney, command.IncomeTaxPercentage);
            //return this.PrintServerConfiguration(rawCommand);
        }

        public Task ShowServerSetup(SocketSlashCommand rawCommand, ShowServerSetupCommand command)
        {
            //return this.PrintServerConfiguration(rawCommand);
        }
        /*
        private Task PrintServerConfiguration(SocketSlashCommand rawCommand)
        {
            if(this.serverConfiguration == null)
            {
                return Task.CompletedTask;
            }

            var embed = new EmbedBuilder()
                .WithTitle("Server configuration")
                .WithFields(new[] 
                { 
                    new EmbedFieldBuilder()
                        .WithName(SetupServerCommand.Param_UserStartMoney) 
                        .WithValue(this.serverConfiguration.UserStartMoney),

                    new EmbedFieldBuilder()
                        .WithName(SetupServerCommand.Param_ServerStartMoney)
                        .WithValue(this.serverConfiguration.ServerStartMoney),

                    new EmbedFieldBuilder()
                        .WithName(SetupServerCommand.Param_IncomeTaxPercentage)
                        .WithValue(this.serverConfiguration.IncomeTaxPercentage),
                })
                .Build();

            return rawCommand.RespondAsync(embed: embed);
        }
        */
    }
}
