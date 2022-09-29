using Economius.BotRunner.Areas.Commons;
using Economius.Domain.Shopping;

namespace Economius.BotRunner.Areas.Shops.Views.Models
{
    public class ShowUserShopViewModel : IViewModel
    {
        public ulong ShopOwnerId { get; internal set; }
        public IEnumerable<Product> Products { get; internal set; }
    }
}