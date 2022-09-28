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
            var arguments = rawCommand.Data.Options.Select(x => (Name: x.Name, Value: x.Value)).ToArray();
            var botCommand = WorkflowTypesContainer.AutoCreateCommand(rawCommand.Data.Name, arguments) as IBotCommand;

            var viewModel = await WorkflowTypesContainer.RunMethodOfCommand(rawCommand, botCommand, rawCommand.Data.Name);
            if(viewModel == null)
            {
                throw new NullReferenceException();
            }
            await WorkflowTypesContainer.RunViewOfModel(rawCommand, viewModel, viewModel.GetType().Name);
        }
    }
}
