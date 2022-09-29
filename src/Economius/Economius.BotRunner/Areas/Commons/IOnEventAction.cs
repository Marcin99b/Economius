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
        IOnEventActionOrder Order { get; }
        void Configure(DiscordSocketClient client);
    }

    public enum IOnEventActionOrder
    {
        Immediate = 0,
        AfterFirstGroup = 1,
        Middletime = 2,
        End = 3,
    }
}
