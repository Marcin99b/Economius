using Discord;
using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using Economius.Domain.Games.TicTacToe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.BotRunner.Areas.Games.TicTacToe
{
    public interface ITicTacToeController : IController
    {
        Task<IViewModel> StartTicTacToe(SocketSlashCommand rawCommand, StartTicTacToeCommand command);
    }

    public class TicTacToeController : ITicTacToeController
    {
        private readonly ITicTacToeService ticTacToeService;

        public TicTacToeController(ITicTacToeService ticTacToeService)
        {
            this.ticTacToeService = ticTacToeService;
        }

        public async Task<IViewModel> StartTicTacToe(SocketSlashCommand rawCommand, StartTicTacToeCommand command)
        {
            var stage = this.ticTacToeService.CreateSession();
            var view = "";
            foreach (var yItem in stage.State.Table)
            {
                foreach (var xItem in yItem)
                {
                    view += xItem switch
                    {
                        TicTacToeOptions.Circle => Emote.Parse("<:o:1028443467627626546>").ToString(),
                        TicTacToeOptions.Cross => Emote.Parse("<:x:1028443467627626546>").ToString(),
                        TicTacToeOptions.None => Emote.Parse("<:black_large_square:1028443610141704213>").ToString(),
                    } + " ";
                }
                view += "\n";
                
            }
            return new SuccessViewModel(view);
        }
    }
}
