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
using System.Text.RegularExpressions;

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
#if DEBUG
            var viewModel = await this.ShowErrorIfCatch(async () => await WorkflowTypesContainer.RunMethodOfCommand(rawCommand, botCommand, rawCommand.Data.Name));
#else
            //todo error processing
            var viewModel = await WorkflowTypesContainer.RunMethodOfCommand(rawCommand, botCommand, rawCommand.Data.Name);
#endif

            if (viewModel == null)
            {
                throw new NullReferenceException();
            }
            await WorkflowTypesContainer.RunViewOfModel(rawCommand, viewModel, viewModel.GetType().Name);
        }

        private async Task<IViewModel> ShowErrorIfCatch(Func<Task<IViewModel>> func)
        {
            try
            {
                return await func.Invoke();
            }
            catch (Exception ex)
            {
                var stackTraceWithoutFullPaths = Regex.Replace(ex.StackTrace ?? string.Empty, @"[A-Z]:((\\|\/)\w+)+(\\|\/)?Economius(\\|\/)src(\\|\/)", string.Empty);
                return new ErrorViewModel($"**Raw errors are visible only in debug mode!**\n\n{ex.Message}\n\n{stackTraceWithoutFullPaths}");
            }
        }
    }
}
