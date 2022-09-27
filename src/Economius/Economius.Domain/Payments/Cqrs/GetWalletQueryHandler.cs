using Economius.Cqrs;
using Economius.Infrastructure.Database;
using MongoDB.Driver;

namespace Economius.Domain.Payments.Cqrs
{
    public class GetWalletQueryHandler : IQueryHandler<GetWalletQuery, GetWalletQueryResult>
    {
        private readonly ISessionFactory sessionFactory;

        public GetWalletQueryHandler(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public GetWalletQueryResult Handle(GetWalletQuery query)
        {
            using var session = this.sessionFactory.CreateMongo();
            if(query.Id.HasValue)
            {
                return new GetWalletQueryResult(session.Get<Wallet>(query.Id.Value));
            }
            var pair = query.UserServerPair!.Value;
            var result = session.Get<Wallet>().FirstOrDefault(x => x.ServerId == pair.ServerId && x.UserId == pair.UserId)
            return new GetWalletQueryResult(result);
        }
    }
}
