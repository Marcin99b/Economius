using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Configuration.Views
{
    public class ShowServerSetupViewModel : IViewModel
    {
        public long UserStartMoney { get; }
        public long ServerStartMoney { get; }
        public long IncomeTaxPercentage { get; }

        public ShowServerSetupViewModel(long userStartMoney, long serverStartMoney, long incomeTaxPercentage)
        {
            this.UserStartMoney = userStartMoney;
            this.ServerStartMoney = serverStartMoney;
            this.IncomeTaxPercentage = incomeTaxPercentage;
        }
    }
}
