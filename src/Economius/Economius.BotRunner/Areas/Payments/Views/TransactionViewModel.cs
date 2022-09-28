using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Payments.Views
{
    public class TransactionViewModel : IViewModel
    {
        public ulong FromUserId { get; }
        public ulong ToUserId { get; }
        public long Amount { get; }
        public string Comment { get; }

        public TransactionViewModel(ulong fromUserId, ulong toUserId, long amount, string comment)
        {
            this.FromUserId = fromUserId;
            this.ToUserId = toUserId;
            this.Amount = amount;
            this.Comment = comment;
        }
    }
}
