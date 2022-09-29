using Economius.Cqrs;

namespace Economius.Domain.Shopping.Cqrs
{
    public class GetShopQuery : IQuery<GetShopQueryResult>
    {
        public Guid WalletId { get; }
        public ulong ServerId { get; }
        public ulong UserId { get; }

        public GetShopQuery(Guid walletId)
        {
            this.WalletId = walletId;
        }

        public GetShopQuery(ulong serverId, ulong userId)
        {
            this.ServerId = serverId;
            this.UserId = userId;
        }
    }
}
