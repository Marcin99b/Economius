using Economius.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.Domain.Configuration
{
    public class ServerConfiguration : Entity
    {
        public ulong ServerId { get; private set; }
        public long UserStartMoney { get; private set; }
        public long ServerStartMoney { get; private set; }
        public long IncomeTaxPercentage { get; private set; }

        public ServerConfiguration(ulong serverId, long userStartMoney, long serverStartMoney, long incomeTaxPercentage)
        {
            this.ServerId = serverId;
            this.UserStartMoney = userStartMoney;
            this.ServerStartMoney = serverStartMoney;
            this.IncomeTaxPercentage = incomeTaxPercentage;
        }
    }
}
