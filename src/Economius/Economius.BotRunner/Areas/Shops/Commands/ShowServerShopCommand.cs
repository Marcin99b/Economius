using Discord;
using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Shops.Commands
{
    public class ShowServerShopCommand : IBotCommand
    {
        public const string CommandName = "show-server-shop";

        public static SlashCommandProperties CreateCommandInfo()
        {
            return new SlashCommandBuilder()
                .WithName(CommandName)
                .WithDescription("Show server shop.")
                .Build();
        }
    }
}
