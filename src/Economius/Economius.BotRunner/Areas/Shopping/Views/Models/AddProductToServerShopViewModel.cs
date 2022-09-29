using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Shopping.Views.Models
{
    public class AddProductToServerShopViewModel : IViewModel
    {
        public string Name { get; }
        public long Price { get; }

        public AddProductToServerShopViewModel(string name, long price)
        {
            this.Name = name;
            this.Price = price;
        }
    }
}