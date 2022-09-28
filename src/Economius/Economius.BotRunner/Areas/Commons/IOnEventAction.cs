using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.BotRunner.Areas.Commons
{
    public interface IOnEventAction
    {
        void Configure(DiscordSocketClient client);
    }
}
