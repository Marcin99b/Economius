using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using Economius.BotRunner.Areas.Configuration.Commands;
using Economius.BotRunner.Areas.Configuration.Controllers;
using Economius.BotRunner.Areas.Configuration.Views;

namespace Economius.BotRunner
{
    public interface ICommandsRouter
    {
        Task Route(SocketSlashCommand rawCommand);
    }

    public class CommandsRouter : ICommandsRouter
    {
        //todo ioc
        private readonly IConfigurationsController configurationsController;
        private readonly IConfigurationsViews configurationsViews;
        private readonly ICommonViews commonViews;

        public CommandsRouter(IConfigurationsController configurationController, 
            IConfigurationsViews configurationsViews, 
            ICommonViews commonViews)
        {
            this.configurationsController = configurationController;
            this.configurationsViews = configurationsViews;
            this.commonViews = commonViews;
        }

        public async Task Route(SocketSlashCommand rawCommand)
        {
            //todo create test that checks if we handle all commands and params
            //todo reflection
            var getViewModelTask = rawCommand.Data.Name switch
            {
                SetupServerCommand.CommandName => this.configurationsController.SetupServer(rawCommand, new SetupServerCommand()
                {
                    UserStartMoney = (dynamic)rawCommand.Data.Options.First(x => x.Name == SetupServerCommand.Param_UserStartMoney).Value,
                    ServerStartMoney = (dynamic)rawCommand.Data.Options.First(x => x.Name == SetupServerCommand.Param_ServerStartMoney).Value,
                    IncomeTaxPercentage = (dynamic)rawCommand.Data.Options.First(x => x.Name == SetupServerCommand.Param_IncomeTaxPercentage).Value
                }),
                ShowServerSetupCommand.CommandName => this.configurationsController.ShowServerSetup(rawCommand, new ShowServerSetupCommand()),
                _ => throw new NotImplementedException()
            };

            var viewModel = await getViewModelTask;
            if(viewModel == null)
            {
                throw new NullReferenceException();
            }

            //todo reflection
            var printViewModelTask = viewModel switch
            {
                var x when x is ShowServerSetupViewModel castedModel => this.configurationsViews.ShowServerSetupView(rawCommand, castedModel),
                var x when x is EmptyViewModel castedModel => this.commonViews.EmptyView(rawCommand, castedModel),
                _ => throw new NotImplementedException()
            };
        }
    }
}
