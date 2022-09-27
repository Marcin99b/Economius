using Economius.Cqrs;

namespace Economius.Domain.Payments.Cqrs
{
    public class GetWalletQuery : IQuery<GetWalletQueryResult>
    {
        public Guid? Id { get; }
        public (ulong ServerId, ulong UserId)? UserServerPair { get; }

        public GetWalletQuery(Guid? id = null, (ulong ServerId, ulong UserId)? userServerPair = null)
        {
            this.Id = id;
            this.UserServerPair = userServerPair;
        }
    }
}
