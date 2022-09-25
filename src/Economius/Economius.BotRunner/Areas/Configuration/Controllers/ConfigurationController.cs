using Discord;
using Discord.WebSocket;
using Economius.BotRunner.Areas.Configuration.Commands;
using Economius.Domain.Configuration;

namespace Economius.BotRunner.Areas.Configuration.Controllers
{
    public interface IConfigurationController
    {
        Task SetupServer(SocketSlashCommand rawCommand, SetupServerCommand command);
    }

    public class ConfigurationController : IConfigurationController
    {
        private ServerConfiguration serverConfiguration; //todo for test

        public Task SetupServer(SocketSlashCommand rawCommand, SetupServerCommand command)
        {
            this.serverConfiguration = new ServerConfiguration(rawCommand.GuildId!.Value, command.UserStartMoney, command.ServerStartMoney, command.IncomeTaxPercentage);
            return this.PrintServerConfiguration(rawCommand);
        }

        public Task ShowServerSetup(SocketSlashCommand rawCommand, ShowServerSetupCommand command)
        {
            return this.PrintServerConfiguration(rawCommand);
        }

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
    }
}
