using Discord.WebSocket;
using Economius.BotRunner.Areas.Configuration.Commands;

namespace Economius.BotRunner.Areas.Configuration.Controllers
{
    public interface IConfigurationController
    {
        Task SetupServer(SocketSlashCommand rawCommand, SetupServerCommand command);
    }

    public class ConfigurationController : IConfigurationController
    {
        public Task SetupServer(SocketSlashCommand rawCommand, SetupServerCommand command)
        {
            return Task.CompletedTask;
        }
    }
}
