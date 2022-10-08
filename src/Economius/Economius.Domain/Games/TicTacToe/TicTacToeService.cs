using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.Domain.Games.TicTacToe
{
    public interface ITicTacToeService
    {
        TicTacToeStage CreateSession();
    }

    public class TicTacToeService : ITicTacToeService
    {
        public TicTacToeStage CreateSession()
        {
            var startTable = new TicTacToeOptions[][]
            {
                new TicTacToeOptions[] { TicTacToeOptions.Cross, TicTacToeOptions.Cross, TicTacToeOptions.Circle, },
                new TicTacToeOptions[] { TicTacToeOptions.Circle, TicTacToeOptions.None, TicTacToeOptions.None, },
                new TicTacToeOptions[] { TicTacToeOptions.Circle, TicTacToeOptions.Cross, TicTacToeOptions.None, },
            };
            var stage = new TicTacToeStage(0, 0, new TicTacToeStageState(0, startTable));
            return stage;
        }
    }
}
