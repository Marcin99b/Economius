using Economius.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.Domain.Payments.Cqrs
{
    public class CreateWalletCommand : ICommand
    {
        public ulong ServerId { get; }
        public ulong UserId { get; }
        public long StartBalance { get; }

        public CreateWalletCommand(ulong serverId, ulong userId, long startBalance)
        {
            this.ServerId = serverId;
            this.UserId = userId;
            this.StartBalance = startBalance;
        }
    }
}
