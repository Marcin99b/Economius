using Economius.Cqrs;

namespace Economius.Domain.Payments.Cqrs
{
    public class GetTransactionsQuery : IQuery<GetTransactionsQueryResult>
    {
        public ulong ServerId { get; }
        public ulong UserId { get; }
        public int Quantity { get; }

        public GetTransactionsQuery(ulong serverId, ulong userId, int quantity)
        {
            this.ServerId = serverId;
            this.UserId = userId;
            this.Quantity = quantity;
        }
    }
}
