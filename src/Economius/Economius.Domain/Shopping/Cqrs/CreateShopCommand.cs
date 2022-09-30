using Economius.Cqrs;
using Economius.Domain.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.Domain.Shopping.Cqrs
{
    public class CreateShopCommand : ICommand
    {
        public Guid WalletId { get; }
        public ulong ServerId { get; }
        public ulong UserId { get; }

        public CreateShopCommand(Guid walletId, ulong serverId, ulong userId)
        {
            this.WalletId = walletId;
            this.ServerId = serverId;
            this.UserId = userId;
        }
    }
}
