using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Payments.Views.Models
{
    public class TransactionViewModel : IViewModel
    {
        public Guid TransactionId { get; }
        public ulong FromUserId { get; }
        public ulong ToUserId { get; }
        public long Amount { get; }
        public string Comment { get; }
        public DateTime CreatedAt { get; }

        public TransactionViewModel(Guid transactionId, ulong fromUserId, ulong toUserId, long amount, string comment, DateTime createdAt)
        {
            this.TransactionId = transactionId;
            this.FromUserId = fromUserId;
            this.ToUserId = toUserId;
            this.Amount = amount;
            this.Comment = comment;
            this.CreatedAt = createdAt;
        }
    }
}
