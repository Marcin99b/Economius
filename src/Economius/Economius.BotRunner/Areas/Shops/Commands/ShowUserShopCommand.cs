using Discord;
using Economius.BotRunner.Areas.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.BotRunner.Areas.Shops.Commands
{
    public class ShowUserShopCommand : IBotCommand
    {
        public const string CommandName = "show-user-shop";

        public IUser User { get; set; }
        public const string Param_User = "user";

        public static SlashCommandProperties CreateCommandInfo()
        {
            return new SlashCommandBuilder()
                .WithName(CommandName)
                .WithDescription("Show user shop.")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_User)
                    .WithDescription("Select user.")
                    .WithType(ApplicationCommandOptionType.User)
                    .WithRequired(true))
                .Build();
        }
    }
}
