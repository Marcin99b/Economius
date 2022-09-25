namespace Economius.Infrastructure.Database
{
    public interface ISessionFactory
    {
        ISession CreateMongo();
    }
}
