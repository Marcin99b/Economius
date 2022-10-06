namespace Economius.Domain.Commons.MultiStages
{
    public interface IStageState
    {
        int StageLevel { get; }
        bool Compare(IStageState obj);
    }
}
