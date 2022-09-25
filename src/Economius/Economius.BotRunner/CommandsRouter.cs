using Discord.WebSocket;
using Economius.BotRunner.Areas.Configuration.Commands;
using Economius.BotRunner.Areas.Configuration.Controllers;

namespace Economius.BotRunner
{
    public class CommandsRouter
    {
        public async Task Route(SocketSlashCommand rawCommand)
        {
            //todo create test that checks if we handle all commands and params
            //todo ioc
            var configurationController = new ConfigurationController();
            var task = rawCommand.Data.Name switch
            {
                SetupServerCommand.CommandName => configurationController.SetupServer(rawCommand, new SetupServerCommand() 
                {
                    UserStartMoney = (dynamic)rawCommand.Data.Options.First(x => x.Name == SetupServerCommand.Param_UserStartMoney).Value,
                    ServerStartMoney = (dynamic)rawCommand.Data.Options.First(x => x.Name == SetupServerCommand.Param_ServerStartMoney).Value,
                    IncomeTaxPercentage = (dynamic)rawCommand.Data.Options.First(x => x.Name == SetupServerCommand.Param_IncomeTaxPercentage).Value
                }),
                _ => Task.CompletedTask
            };
        }
    }
}
