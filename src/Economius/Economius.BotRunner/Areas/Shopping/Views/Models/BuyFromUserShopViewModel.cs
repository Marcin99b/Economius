using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Shopping.Views.Models
{
    public class BuyFromUserShopViewModel : IViewModel
    {
        public ulong ShopOwnerId { get; internal set; }
        public string Identifier { get; }
        public string Description { get; }
        public long Price { get; }

        public BuyFromUserShopViewModel(ulong shopOwnerId, string identifier, string description, long price)
        {
            this.ShopOwnerId = shopOwnerId;
            this.Identifier = identifier;
            this.Description = description;
            this.Price = price;
        }
    }
}