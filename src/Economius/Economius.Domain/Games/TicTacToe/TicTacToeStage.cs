using Economius.Domain.Commons.MultiStages;

namespace Economius.Domain.Games.TicTacToe
{
    public class TicTacToeStage : Stage<TicTacToeStageState>
    {
        public TicTacToeStage(ulong serverId, ulong userId, TicTacToeStageState startState) : base(serverId, userId, startState)
        {
        }
    }
}
