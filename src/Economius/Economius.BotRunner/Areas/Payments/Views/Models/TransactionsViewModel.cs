using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Payments.Views.Models
{
    public class TransactionsViewModel : IViewModel
    {
        public IEnumerable<TransactionViewModel> transactionViewModels { get; }

        public TransactionsViewModel(IEnumerable<TransactionViewModel> transactionViewModels)
        {
            this.transactionViewModels = transactionViewModels;
        }
    }
}
