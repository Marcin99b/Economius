using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Shopping.Views.Models
{
    public class BuyFromUserShopViewModel : IViewModel
    {
        public ulong ShopOwnerId { get; internal set; }
        public string Name { get; }
        public long Price { get; }

        public BuyFromUserShopViewModel(ulong shopOwnerId, string name, long price)
        {
            this.ShopOwnerId = shopOwnerId;
            this.Name = name;
            this.Price = price;
        }
    }
}