using Discord.WebSocket;
using Economius.BotRunner.Areas.Configuration.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.BotRunner.Areas.Commons
{
    public class EmptyViewModel : IViewModel
    {
    }

    public class CommonViews : ICommonViews
    {
        public Task EmptyView(SocketSlashCommand rawCommand, EmptyViewModel model)
        {
            return Task.CompletedTask;
        }
    }
}
