using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Payments.Views
{
    public class ShowWalletViewModel : IViewModel
    {
        public ulong UserId { get; }
        public long Balance { get; }

        public ShowWalletViewModel(ulong userId, long balance)
        {
            this.UserId = userId;
            this.Balance = balance;
        }
    }
}
