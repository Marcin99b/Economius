using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using Economius.BotRunner.Areas.Configuration.Commands;
using Economius.BotRunner.Areas.Configuration.Controllers;
using Economius.BotRunner.Areas.Configuration.Views;
using Economius.BotRunner.Areas.Payments.Commands;
using Economius.BotRunner.Areas.Payments.Controllers;
using Economius.BotRunner.Areas.Payments.Views;
using MongoDB.Driver;
using System.Reflection;

namespace Economius.BotRunner
{
    public interface ICommandsRouter
    {
        Task Route(SocketSlashCommand rawCommand);
    }

    public class CommandsRouter : ICommandsRouter
    {
        public async Task Route(SocketSlashCommand rawCommand)
        {
            //todo create test that checks if we handle all commands and params
            //todo reflection
            var botCommand = rawCommand.Data.Name switch
            {
                SetupServerCommand.CommandName => new SetupServerCommand()
                {
                    UserStartMoney = (dynamic)rawCommand.Data.Options.First(x => x.Name == SetupServerCommand.Param_UserStartMoney).Value,
                    ServerStartMoney = (dynamic)rawCommand.Data.Options.First(x => x.Name == SetupServerCommand.Param_ServerStartMoney).Value,
                    IncomeTaxPercentage = (dynamic)rawCommand.Data.Options.First(x => x.Name == SetupServerCommand.Param_IncomeTaxPercentage).Value
                } as IBotCommand,
                ShowServerSetupCommand.CommandName => new ShowServerSetupCommand() as IBotCommand,
                ShowWalletCommand.CommandName => new ShowWalletCommand()
                {
                    User = (dynamic?) rawCommand.Data.Options.FirstOrDefault(x => x.Name == ShowWalletCommand.Param_User)?.Value,
                } as IBotCommand,
                TransactionCommand.CommandName => new TransactionCommand()
                {
                    ToUser = (dynamic)rawCommand.Data.Options.First(x => x.Name == TransactionCommand.Param_ToUser).Value,
                    Amount = (dynamic)rawCommand.Data.Options.First(x => x.Name == TransactionCommand.Param_Amount).Value,
                    Comment = (dynamic)rawCommand.Data.Options.First(x => x.Name == TransactionCommand.Param_Comment).Value,
                } as IBotCommand,
                _ => throw new NotImplementedException()
            };

            var viewModel = await WorkflowTypesContainer.RunMethodOfCommand(rawCommand, botCommand, rawCommand.Data.Name);
            if(viewModel == null)
            {
                throw new NullReferenceException();
            }
            await WorkflowTypesContainer.RunViewOfModel(rawCommand, viewModel, viewModel.GetType().Name);
        }
    }
}
