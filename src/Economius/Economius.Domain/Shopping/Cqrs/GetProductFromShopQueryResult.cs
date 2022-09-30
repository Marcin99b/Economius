using Economius.Cqrs;

namespace Economius.Domain.Shopping.Cqrs
{
    public class GetProductFromShopQueryResult : IQueryResult
    {
        public Guid ShopId { get; set; }
        public Guid WalletId { get; set; }
        public Product Product { get; }

        public GetProductFromShopQueryResult(Guid shopId, Guid walletId, Product product)
        {
            this.ShopId = shopId;
            this.WalletId = walletId;
            this.Product = product;
        }
    }
}
