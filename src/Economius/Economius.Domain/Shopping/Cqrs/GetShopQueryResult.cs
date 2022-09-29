using Economius.Cqrs;

namespace Economius.Domain.Shopping.Cqrs
{
    public class GetShopQueryResult : IQueryResult
    {
        public Shop? Shop { get; }

        public GetShopQueryResult(Shop? shop)
        {
            this.Shop = shop;
        }
    }
}
