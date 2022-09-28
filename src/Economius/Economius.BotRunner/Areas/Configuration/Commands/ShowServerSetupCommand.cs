using Discord;
using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Configuration.Commands
{
    public class ShowServerSetupCommand : IBotCommand
    {
        public const string CommandName = "show-server-setup";

        public static SlashCommandProperties CreateCommandInfo()
        {
            return new SlashCommandBuilder()
                .WithName(CommandName)
                .WithDescription("Show server configuration.")
                .Build();
        }
    }
}
