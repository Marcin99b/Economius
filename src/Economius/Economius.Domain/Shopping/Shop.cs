using Economius.Infrastructure.Database.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.Domain.Shopping
{
    public class Shop : Entity
    {
        public ulong ServerId { get; private set; }
        public ulong UserId { get; private set; }
        public ulong WalletId { get; private set; }
        public IEnumerable<Product> Products { get; private set; }

        public Shop(ulong serverId, ulong userId, ulong walletId)
        {
            this.ServerId = serverId;
            this.UserId = userId;
            this.WalletId = walletId;
            this.Products = new List<Product>();
        }
    }
}
