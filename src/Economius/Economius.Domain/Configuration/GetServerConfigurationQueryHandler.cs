using Economius.Cqrs;
using Economius.Infrastructure.Database;
using MongoDB.Driver;

namespace Economius.Domain.Configuration
{
    public class GetServerConfigurationQueryHandler : IQueryHandler<GetServerConfigurationQuery, GetServerConfigurationQueryResult>
    {
        private readonly ISessionFactory sessionFactory;

        public GetServerConfigurationQueryHandler(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public GetServerConfigurationQueryResult Handle(GetServerConfigurationQuery query)
        {
            using var session = this.sessionFactory.CreateMongo();
            var result = session.Get<ServerConfiguration>().FirstOrDefault(x => x.ServerId == query.ServerId);
            return new GetServerConfigurationQueryResult(result);
        }
    }
}
