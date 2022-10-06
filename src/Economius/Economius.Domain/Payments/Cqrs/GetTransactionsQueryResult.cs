using Economius.Cqrs;

namespace Economius.Domain.Payments.Cqrs
{
    public class GetTransactionsQueryResult : IQueryResult
    {
        public IEnumerable<Transaction> Transactions { get; }

        public GetTransactionsQueryResult(IEnumerable<Transaction> transactions)
        {
            this.Transactions = transactions;
        }
    }
}
