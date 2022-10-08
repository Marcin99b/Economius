using Discord;
using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class TicTacToeController
    {
        public async Task<IViewModel> StartTicTacToe(SocketSlashCommand rawCommand, StartTicTacToeCommand command)
        {

            return new SuccessViewModel("Created");
        }
    }
}
