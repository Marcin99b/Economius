using Economius.Infrastructure.Database.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.Domain.Configurations
{
    public class ServerConfiguration : Entity
    {
        public ulong ServerId { get; private set; }
        public long IncomeTaxPercentage { get; private set; }

        public ServerConfiguration(ulong serverId, long incomeTaxPercentage)
        {
            this.ServerId = serverId;
            this.IncomeTaxPercentage = incomeTaxPercentage;
        }

        public void SetIncomeTaxPercentage(long value)
        {
            if (this.IncomeTaxPercentage == value)
            {
                return;
            }
            this.IncomeTaxPercentage = value;
            this.Update();
        }
    }
}
