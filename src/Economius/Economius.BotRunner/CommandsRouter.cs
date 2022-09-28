using Autofac;
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
                    User = (dynamic?) rawCommand.Data.Options.FirstOrDefault(x => x.Name == ShowWalletCommand.Param_User)?.Value ?? null,
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

    public static class WorkflowTypesContainer
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static (string CommandName,Func<SocketSlashCommand, IBotCommand, Task<IViewModel>> Function)[] generatedControllersMethods;
        private static (string ViewName,Func<SocketSlashCommand, IViewModel, Task> Function)[] generatedViewsMethods;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public static void Inject(IContainer container, Type[] controllers, Type[] views)
        {
            generatedControllersMethods = controllers.Select(controllerType =>
            {
                var controllerInstance = container.Resolve(controllerType);
                var methods = controllerType.GetMethods()
                .Where(x => x.GetParameters().Any(x => x.ParameterType.IsAssignableTo(typeof(IBotCommand))))
                .Select(method =>
                {
                    var botCommandType = method
                        .GetParameters()
                        .First(x => x.ParameterType.IsAssignableTo(typeof(IBotCommand))).ParameterType;
                    var commandName = (string)botCommandType.GetField("CommandName")!.GetValue(botCommandType)!;
                    var functionToInvoke = (SocketSlashCommand slashCommand, IBotCommand botCommand)
                        => method.Invoke(controllerInstance, new object[]
                        {
                            slashCommand,
                            botCommand
                        }) as Task<IViewModel>;
                    return (CommandName: commandName, Function: functionToInvoke);
                }).ToArray();
                return methods;
            }).SelectMany(x => x).ToArray()!;

            generatedViewsMethods = views.Select(viewType =>
            {
                var viewInstance = container.Resolve(viewType);
                var methods = viewType.GetMethods()
                .Where(x => x.GetParameters().Any(x => x.ParameterType.IsAssignableTo(typeof(IViewModel))))
                .Select(method =>
                {
                    var viewType = method
                        .GetParameters()
                        .First(x => x.ParameterType.IsAssignableTo(typeof(IViewModel))).ParameterType!;
                    var functionToInvoke = (SocketSlashCommand slashCommand, IViewModel model)
                        => method.Invoke(viewInstance, new object[]
                        {
                            slashCommand,
                            model
                        }) as Task;
                    return (ViewName: viewType.Name, Function: functionToInvoke);
                }).ToArray();
                return methods;
            }).SelectMany(x => x).ToArray()!;
        }
        
        public static Task<IViewModel> RunMethodOfCommand(SocketSlashCommand slashCommand, IBotCommand command, string commandName)
        {
            foreach (var commandMethod in generatedControllersMethods)
            {
                if(commandMethod.CommandName == commandName)
                {
                    return commandMethod.Function.Invoke(slashCommand, command);
                }
            }
            throw new NotImplementedException();
        }

        public static Task RunViewOfModel(SocketSlashCommand slashCommand, IViewModel model, string viewName)
        {
            foreach (var viewMethod in generatedViewsMethods)
            {
                if (viewMethod.ViewName == viewName)
                {
                    return viewMethod.Function.Invoke(slashCommand, model);
                }
            }
            throw new NotImplementedException();
        }
    }
}
