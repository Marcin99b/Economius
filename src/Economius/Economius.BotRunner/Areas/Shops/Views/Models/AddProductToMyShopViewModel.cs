using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Shops.Views.Models
{
    public class AddProductToMyShopViewModel : IViewModel
    {
        public string Name { get; }
        public long Price { get; }

        public AddProductToMyShopViewModel(string name, long price)
        {
            this.Name = name;
            this.Price = price;
        }
    }
}
