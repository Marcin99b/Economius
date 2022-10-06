using Economius.Cqrs;
using Economius.Infrastructure.Database;

namespace Economius.Domain.Payments.Cqrs
{
    public class GetTransactionsQueryHandler : IQueryHandler<GetTransactionsQuery, GetTransactionsQueryResult>
    {
        private readonly ISessionFactory sessionFactory;

        public GetTransactionsQueryHandler(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public GetTransactionsQueryResult Handle(GetTransactionsQuery query)
        {
            using var session = this.sessionFactory.CreateMongo();
            var transactions = session.Get<Transaction>()
                .Where(x => x.ServerId == query.ServerId && (x.FromUserId == query.UserId || x.ToUserId == query.UserId))
                .OrderByDescending(x => x.CreatedAt)
                .Take(query.Quantity);
            return new GetTransactionsQueryResult(transactions);
        }
    }
}
