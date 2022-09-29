using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Configuration.Views.Models
{
    public class ShowServerSetupViewModel : IViewModel
    {
        public long IncomeTaxPercentage { get; }

        public ShowServerSetupViewModel(long incomeTaxPercentage)
        {
            this.IncomeTaxPercentage = incomeTaxPercentage;
        }
    }
}
