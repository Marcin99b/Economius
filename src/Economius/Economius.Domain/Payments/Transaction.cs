using Economius.Infrastructure.Database.Abstraction;

namespace Economius.Domain.Payments
{
    public class Transaction : ImmutableEntity
    {
        public ulong ServerId { get; private set; }
        public ulong FromUserId { get; private set; }
        public ulong ToUserId { get; private set; }
        public long Amount { get; private set; }
        public string Comment { get; private set; }
        public bool IsServerIncreaseBalanceTransaction { get; set; }

        public Transaction(ulong serverId, ulong fromUserId, ulong toUserId, long amount, string comment)
        {
            this.ServerId = serverId;
            this.FromUserId = fromUserId;
            this.ToUserId = toUserId;
            this.Amount = amount;
            this.Comment = comment;
            this.IsServerIncreaseBalanceTransaction = fromUserId == 0 && toUserId == 0;
        }
    }
}
