using Economius.BotRunner.Areas.Commons;
using Economius.Domain.Shopping;

namespace Economius.BotRunner.Areas.Shops.Views.Models
{
    public class ShowUserShopViewModel : IViewModel
    {
        public ulong ShopOwnerId { get; }
        public IEnumerable<Product> Products { get; }

        public ShowUserShopViewModel(ulong shopOwnerId, IEnumerable<Product> products)
        {
            this.ShopOwnerId = shopOwnerId;
            this.Products = products;
        }
    }
}