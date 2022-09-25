using Autofac;
using Economius.Infrastructure.Database;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Economius.BotRunner.IoC
{
    [ExcludeFromCodeCoverage]
    public class DatabaseModule : Autofac.Module
    {
        private readonly string mongoConnectionString;

        public DatabaseModule(string mongoConnectionString)
        {
            this.mongoConnectionString = mongoConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(DatabaseModule)
                .GetTypeInfo()
                .Assembly;

            if (!string.IsNullOrWhiteSpace(this.mongoConnectionString))
            {
                builder.Register((c, p) => new MongoClient(this.mongoConnectionString).GetDatabase("Economius"))
                    .As<IMongoDatabase>()
                    .SingleInstance();
            }

            builder.RegisterType<SessionFactory>()
                .As<ISessionFactory>()
                .PreserveExistingDefaults()
                .SingleInstance();
        }
    }
}
