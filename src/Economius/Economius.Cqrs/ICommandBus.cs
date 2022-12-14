using System.Threading.Tasks;

namespace Economius.Cqrs
{
    public interface ICommandBus
    {
        Task ExecuteAsync<T>(T command) where T : ICommand;
        Task AddToSingleThreadQueue<T>(T command) where T : ICommand;
    }
}
