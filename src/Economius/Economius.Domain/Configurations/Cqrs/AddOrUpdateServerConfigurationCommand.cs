using Economius.Cqrs;

namespace Economius.Domain.Configurations.Cqrs
{
    public class AddOrUpdateServerConfigurationCommand : ICommand
    {
        public ulong ServerId { get; }
        public long IncomeTaxPercentage { get; }

        public AddOrUpdateServerConfigurationCommand(ulong serverId, long incomeTaxPercentage)
        {
            this.ServerId = serverId;
            this.IncomeTaxPercentage = incomeTaxPercentage;
        }
    }
}
