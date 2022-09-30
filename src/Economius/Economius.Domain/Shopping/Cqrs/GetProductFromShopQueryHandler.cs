using Economius.Cqrs;
using Economius.Infrastructure.Database;

namespace Economius.Domain.Shopping.Cqrs
{
    public class GetProductFromShopQueryHandler : IQueryHandler<GetProductFromShopQuery, GetProductFromShopQueryResult>
    {
        private readonly ISessionFactory sessionFactory;

        public GetProductFromShopQueryHandler(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public GetProductFromShopQueryResult Handle(GetProductFromShopQuery query)
        {
            using var session = this.sessionFactory.CreateMongo();

            var shop = query.WalletId != default
                ? session.Get<Shop>(query.WalletId)!
                : session.Get<Shop>()
                    .FirstOrDefault(x => x.ServerId == query.ServerId && x.UserId == query.UserId)
                    ?? throw new ArgumentException($"Shop for user {query.UserId} does not exist on server {query.ServerId}.");

            var product = shop.Products.FirstOrDefault(x => x.Identifier == query.Identifier);
            if (product == null)
            {
                throw new ArgumentException($"Product {query.Identifier} does not exist in shop.");
            }
            if (product.Price != query.Price)
            {
                throw new ArgumentException($"Product {product.Identifier} has price {product.Price} instead {query.Price}.");
            }

            return new GetProductFromShopQueryResult(shop.Id, shop.WalletId, product);
        }
    }
}
