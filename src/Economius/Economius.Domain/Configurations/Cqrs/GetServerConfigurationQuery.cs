using Economius.Cqrs;

namespace Economius.Domain.Configurations.Cqrs
{
    public class GetServerConfigurationQuery : IQuery<GetServerConfigurationQueryResult>
    {
        public ulong ServerId { get; }

        public GetServerConfigurationQuery(ulong serverId)
        {
            this.ServerId = serverId;
        }
    }
}
