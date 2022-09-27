using Economius.Cqrs;

namespace Economius.Domain.Configurations.Cqrs
{
    public class GetServerConfigurationQueryResult : IQueryResult
    {
        public ServerConfiguration? ServerConfiguration { get; }

        public GetServerConfigurationQueryResult(ServerConfiguration? serverConfiguration)
        {
            this.ServerConfiguration = serverConfiguration;
        }
    }
}
