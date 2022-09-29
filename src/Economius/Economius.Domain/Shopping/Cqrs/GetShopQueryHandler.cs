using Economius.Cqrs;
using Economius.Infrastructure.Database;

namespace Economius.Domain.Shopping.Cqrs
{
    public class GetShopQueryHandler : IQueryHandler<GetShopQuery, GetShopQueryResult>
    {
        private readonly ISessionFactory sessionFactory;

        public GetShopQueryHandler(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public GetShopQueryResult Handle(GetShopQuery query)
        {
            using var session = this.sessionFactory.CreateMongo();
            if(query.WalletId != default)
            {
                return new GetShopQueryResult(session.Get<Shop>().FirstOrDefault(x => x.WalletId == query.WalletId));
            }
            return new GetShopQueryResult(session.Get<Shop>().FirstOrDefault(x => x.ServerId == query.ServerId && x.UserId == query.UserId));
        }
    }
}
