using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using Economius.BotRunner.Areas.Configuration.Commands;
using Economius.BotRunner.Areas.Configuration.Controllers;
using Economius.BotRunner.Areas.Configuration.Views;
using Economius.BotRunner.Areas.Payments.Commands;
using Economius.BotRunner.Areas.Payments.Controllers;
using Economius.BotRunner.Areas.Payments.Views;

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
        private readonly IWalletsController walletsController;
        private readonly IWalletsViews walletsViews;
        private readonly ICommonViews commonViews;

        public CommandsRouter(IConfigurationsController configurationController, 
            IConfigurationsViews configurationsViews, 
            IWalletsController walletsController,
            IWalletsViews walletsViews,
            ICommonViews commonViews)
        {
            this.configurationsController = configurationController;
            this.configurationsViews = configurationsViews;
            this.walletsController = walletsController;
            this.walletsViews = walletsViews;
            this.commonViews = commonViews;
        }

        public async Task Route(SocketSlashCommand rawCommand)
        {
            //todo create test that checks if we handle all commands and params
            //todo reflection
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            var getViewModelTask = rawCommand.Data.Name switch
            {
                SetupServerCommand.CommandName => this.configurationsController.SetupServer(rawCommand, new SetupServerCommand()
                {
                    UserStartMoney = (dynamic)rawCommand.Data.Options.First(x => x.Name == SetupServerCommand.Param_UserStartMoney).Value,
                    ServerStartMoney = (dynamic)rawCommand.Data.Options.First(x => x.Name == SetupServerCommand.Param_ServerStartMoney).Value,
                    IncomeTaxPercentage = (dynamic)rawCommand.Data.Options.First(x => x.Name == SetupServerCommand.Param_IncomeTaxPercentage).Value
                }),
                ShowServerSetupCommand.CommandName => this.configurationsController.ShowServerSetup(rawCommand, new ShowServerSetupCommand()),
                ShowWalletCommand.CommandName => this.walletsController.ShowWallet(rawCommand, new ShowWalletCommand()
                {
                    User = (dynamic) rawCommand.Data.Options.FirstOrDefault(x => x.Name == ShowWalletCommand.Param_User),
                }),
                _ => throw new NotImplementedException()
            };
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            var viewModel = await getViewModelTask;
            if(viewModel == null)
            {
                throw new NullReferenceException();
            }

            //todo reflection
            var printViewModelTask = viewModel switch
            {
                var x when x is ShowServerSetupViewModel castedModel => this.configurationsViews.ShowServerSetupView(rawCommand, castedModel),
                var x when x is ShowWalletViewModel castedModel => this.walletsViews.ShowWalletView(rawCommand, castedModel),

                var x when x is EmptyViewModel castedModel => this.commonViews.EmptyView(rawCommand, castedModel),
                var x when x is SuccessViewModel castedModel => this.commonViews.Success(rawCommand, castedModel),
                var x when x is ErrorViewModel castedModel => this.commonViews.Error(rawCommand, castedModel),

                _ => throw new NotImplementedException()
            };
        }
    }
}
