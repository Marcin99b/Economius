namespace Economius.Domain.Commons.MultiStages
{
    public class TicTacToeStage : Stage<TicTacToeStageState>
    {
        public TicTacToeStage(ulong serverId, ulong userId, TicTacToeStageState startState) : base(serverId, userId, startState)
        {
        }
    }
}
