using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Shopping.Views.Models
{
    public class RemoveProductFromMyShopViewModel : IViewModel
    {
        public string Identifier { get; }

        public RemoveProductFromMyShopViewModel(string identifier)
        {
            this.Identifier = identifier;
        }
    }
}