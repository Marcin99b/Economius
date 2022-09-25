using Economius.Cqrs;

namespace Economius.Domain.Configuration
{
    public class AddOrUpdateServerConfigurationCommand : ICommand
    {
        public ulong ServerId { get; }
        public long UserStartMoney { get; }
        public long ServerStartMoney { get; }
        public long IncomeTaxPercentage { get; }

        public AddOrUpdateServerConfigurationCommand(ulong serverId, long userStartMoney, long serverStartMoney, long incomeTaxPercentage)
        {
            this.ServerId = serverId;
            this.UserStartMoney = userStartMoney;
            this.ServerStartMoney = serverStartMoney;
            this.IncomeTaxPercentage = incomeTaxPercentage;
        }
    }
}
