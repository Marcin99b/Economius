using Discord.WebSocket;

namespace Economius.BotRunner.Areas.Commons
{
    public interface ICommonViews
    {
        Task EmptyView(SocketSlashCommand rawCommand, EmptyViewModel model);
    }
}