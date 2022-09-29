using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Shops.Views.Models
{
    public class RemoveProductFromMyShopViewModel : IViewModel
    {
        public string Name { get; }

        public RemoveProductFromMyShopViewModel(string name)
        {
            this.Name = name;
        }
    }
}