using Economius.Cqrs;

namespace Economius.Domain.Payments.Cqrs
{
    public class RecalculateWalletBalanceCommand : ICommand
    {
        public ulong ServerId { get; }
        public ulong UserId { get; }

        public RecalculateWalletBalanceCommand(ulong serverId, ulong userId)
        {
            this.ServerId = serverId;
            this.UserId = userId;
        }
    }
}
