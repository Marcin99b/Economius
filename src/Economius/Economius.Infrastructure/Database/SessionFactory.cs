using Economius.Infrastructure.Database.MongoDB;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace Economius.Infrastructure.Database
{
    [ExcludeFromCodeCoverage]
    public class SessionFactory : ISessionFactory
    {
        private readonly IMongoDatabase mongoDatabase;

        public SessionFactory(IMongoDatabase mongoDatabase)
        {
            this.mongoDatabase = mongoDatabase;
        }

        public ISession CreateMongo()
        {
            return new MongoSession(this.mongoDatabase);
        }
    }
}
