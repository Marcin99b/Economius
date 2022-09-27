using Economius.Infrastructure.Database.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.Domain.Payments
{
    public class Wallet : Entity
    {
        public ulong ServerId { get; private set; }
        public ulong UserId { get; private set; }
        public long Balance { get; private set; }

        public Wallet(ulong serverId, ulong userId)
        {
            this.ServerId = serverId;
            this.UserId = userId;
        }

        public void SetBalance(long value)
        {
            if (this.Balance == value)
            {
                return;
            }
            this.Balance = value;
            this.Update();
        }
    }
}
