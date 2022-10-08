using Economius.Domain.Commons.MultiStages;

namespace Economius.Domain.Games.TicTacToe
{
    public class TicTacToeStageState : IStageState
    {
        public int StageLevel { get; }
        public TicTacToeOptions[][] Table { get; }

        public TicTacToeStageState(int stageLevel, TicTacToeOptions[][] table)
        {
            this.StageLevel = stageLevel;
            this.Table = table;
        }

        public static TicTacToeStageState Default => new TicTacToeStageState(0, Array.Empty<TicTacToeOptions[]>());

        public bool Compare(IStageState obj)
        {
            throw new NotImplementedException();
        }
    }
}
