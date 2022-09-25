using Discord;

namespace Economius.BotRunner.Areas.Configuration.Commands
{
    public class ShowServerSetupCommand
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
