using Autofac;
using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using MongoDB.Driver;

namespace Economius.BotRunner
{
    public static class WorkflowTypesContainer
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static (string CommandName, Func<IBotCommand> CreateInstance, (string Name, Action<object, object> SetCommandValue)[] Parameters)[] generatedCommandsFunctions;
        private static (string CommandName,Func<SocketSlashCommand, IBotCommand, Task<IViewModel>> Function)[] generatedControllersMethods;
        private static (string ViewName,Func<SocketSlashCommand, IViewModel, Task> Function)[] generatedViewsMethods;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public static void Inject(IContainer container, Type[] controllers, Type[] views, Type[] botCommands)
        {
            SetupCommands(container, botCommands);
            SetupControllers(container, controllers);
            SetupViews(container, views);
        }

        public static IBotCommand AutoCreateCommand(string commandName, (string Name, object Value)[] arguments)
        {
            //todo performance check and improve
            foreach (var commandInfo in generatedCommandsFunctions)
            {
                if (commandInfo.CommandName == commandName)
                {
                    var instance = commandInfo.CreateInstance();
                    foreach (var parameter in commandInfo.Parameters)
                    {
                        foreach (var argument in arguments)
                        {
                            if (parameter.Name == argument.Name)
                            {
                                parameter.SetCommandValue(instance, argument.Value);
                            }
                        }
                    }
                    return instance;
                }
            }
            throw new NotImplementedException();
        }

        public static Task<IViewModel> RunMethodOfCommand(SocketSlashCommand slashCommand, IBotCommand command, string commandName)
        {
            foreach (var (CommandName, Function) in generatedControllersMethods)
            {
                if(CommandName == commandName)
                {
                    return Function.Invoke(slashCommand, command);
                }
            }
            throw new NotImplementedException();
        }

        public static Task RunViewOfModel(SocketSlashCommand slashCommand, IViewModel model, string viewName)
        {
            foreach (var (ViewName, Function) in generatedViewsMethods)
            {
                if (ViewName == viewName)
                {
                    return Function.Invoke(slashCommand, model);
                }
            }
            throw new NotImplementedException();
        }

        private static void SetupControllers(IContainer container, Type[] controllers)
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
        }

        private static void SetupViews(IContainer container, Type[] views)
        {
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

        private static void SetupCommands(IContainer container, Type[] commands)
        {
            generatedCommandsFunctions = commands.Select(commandType =>
            {
                var commandName = (string)commandType.GetField("CommandName")!.GetValue(commandType)!;
                var createInstance = () => Activator.CreateInstance(commandType) as IBotCommand;
                var parameters = commandType
                    .GetFields()
                    .Where(x => x.Name.StartsWith("Param_"))
                    .Select(x => (
                        Name: (string)x.GetValue(commandType)!,
                        SetCommandValue: new Action<object, object>((instance, value) =>
                        {
                            commandType
                                .GetProperty(x.Name.Replace("Param_", string.Empty))!
                                .SetValue(instance, value);
                        }))).ToArray();
                return (CommandName: commandName, CreateInstance: createInstance, Parameters: parameters);
            }).ToArray();
        }
    }
}
