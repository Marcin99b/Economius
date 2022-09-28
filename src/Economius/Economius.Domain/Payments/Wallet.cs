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
        public bool IsServerWallet { get; private set; }

        public Wallet(ulong serverId, ulong userId)
        {
            this.ServerId = serverId;
            this.UserId = userId;
            if(userId == 0)
            {
                this.IsServerWallet = true;
            }
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
