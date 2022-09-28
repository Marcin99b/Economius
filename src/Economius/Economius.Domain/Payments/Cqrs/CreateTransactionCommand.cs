using Economius.Cqrs;

namespace Economius.Domain.Payments.Cqrs
{
    public class CreateTransactionCommand : ICommand
    {
        public ulong ServerId { get; }
        public ulong FromUserId { get; }
        public ulong ToUserId { get; }
        public long Amount { get; }
        public string Comment { get; set; }

        public CreateTransactionCommand(ulong serverId, ulong fromUserId, ulong toUserId, long amount, string comment)
        {
            this.ServerId = serverId;
            this.FromUserId = fromUserId;
            this.ToUserId = toUserId;
            this.Amount = amount;
            this.Comment = comment;
        }
    }
}
