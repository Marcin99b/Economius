using Discord.WebSocket;
using Economius.BotRunner.Areas.Configuration.Commands;
using Economius.BotRunner.Areas.Configuration.Controllers;

namespace Economius.BotRunner
{
    public interface ICommandsRouter
    {
        Task Route(SocketSlashCommand rawCommand);
    }

    public class CommandsRouter : ICommandsRouter
    {
        //todo ioc
        private readonly IConfigurationController configurationController;

        public CommandsRouter(IConfigurationController configurationController)
        {
            this.configurationController = configurationController;
        }

        public async Task Route(SocketSlashCommand rawCommand)
        {
            //todo create test that checks if we handle all commands and params
            var task = rawCommand.Data.Name switch
            {
                SetupServerCommand.CommandName => this.configurationController.SetupServer(rawCommand, new SetupServerCommand()
                {
                    UserStartMoney = (dynamic)rawCommand.Data.Options.First(x => x.Name == SetupServerCommand.Param_UserStartMoney).Value,
                    ServerStartMoney = (dynamic)rawCommand.Data.Options.First(x => x.Name == SetupServerCommand.Param_ServerStartMoney).Value,
                    IncomeTaxPercentage = (dynamic)rawCommand.Data.Options.First(x => x.Name == SetupServerCommand.Param_IncomeTaxPercentage).Value
                }),
                ShowServerSetupCommand.CommandName => this.configurationController.ShowServerSetup(rawCommand, new ShowServerSetupCommand()),
                _ => throw new NotImplementedException()
            };
        }
    }
}
