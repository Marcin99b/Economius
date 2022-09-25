using System.Threading.Tasks;

namespace Economius.Cqrs
{
    public interface IQueryBus
    {
        W Execute<W>(IQuery<W> query) where W : IQueryResult;
        Task<W> ExecuteAsync<W>(IQuery<W> query) where W : IQueryResult;
    }

}