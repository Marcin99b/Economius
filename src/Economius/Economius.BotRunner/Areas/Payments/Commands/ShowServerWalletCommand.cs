using Discord;
using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Payments.Commands
{
    public class ShowServerWalletCommand : IBotCommand
    {
        public const string CommandName = "show-server-wallet";

        public static SlashCommandProperties CreateCommandInfo()
        {
            return new SlashCommandBuilder()
                .WithName(CommandName)
                .WithDescription("Show server's wallet.")
                .Build();
        }
    }
}
