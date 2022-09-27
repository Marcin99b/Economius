using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.BotRunner.Areas.Payments.Commands
{
    public class ShowWalletCommand
    {
        public const string CommandName = "show-wallet";

        public IUser? User { get; set; }
        public const string Param_User = "user";

        public static SlashCommandProperties CreateCommandInfo()
        {
            return new SlashCommandBuilder()
                .WithName(CommandName)
                .WithDescription("Show user's wallet.")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_User)
                    .WithDescription("Select user.")
                    .WithType(ApplicationCommandOptionType.User)
                    .WithRequired(false))
                .Build();
        }
    }
}
