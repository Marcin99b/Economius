using Discord.WebSocket;
using Economius.BotRunner.Areas.Configuration.Commands;

namespace Economius.BotRunner.Areas.Configuration.Controllers
{
    public class ConfigurationController
    {
        public Task SetupServer(SocketSlashCommand rawCommand, SetupServerCommand command)
        {
            return Task.CompletedTask;
        }
    }
}
