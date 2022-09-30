using Economius.Cqrs;

namespace Economius.Domain.Shopping.Cqrs
{
    //todo optional validation
    public class GetProductFromShopQuery : IQuery<GetProductFromShopQueryResult>
    {
        public Guid WalletId { get; }
        public ulong ServerId { get; }
        public ulong UserId { get; }
        public string Identifier { get; }
        public long Price { get; }

        private GetProductFromShopQuery(string identifier, long price)
        {
            this.Identifier = identifier;
            this.Price = price;
        }

        public GetProductFromShopQuery(Guid walletId, string identifier, long price)
            : this(identifier, price)
        {
            this.WalletId = walletId;
        }

        public GetProductFromShopQuery(ulong serverId, ulong userId, string identifier, long price)
            : this(identifier, price)
        {
            this.ServerId = serverId;
            this.UserId = userId;
        }
    }
}
