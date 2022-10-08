using Discord;
using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Games.TicTacToe
{
    public class StartTicTacToeCommand : IBotCommand
    {
        public const string CommandName = "start-tic-tac-toe";

        public static SlashCommandProperties CreateCommandInfo()
        {
            return new SlashCommandBuilder()
                .WithName(CommandName)
                .WithDescription("Start tic tac toe.")
                .Build();
        }
    }
}
