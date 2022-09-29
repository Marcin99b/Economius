using Economius.BotRunner.Areas.Commons;
using Economius.Domain.Shopping;

namespace Economius.BotRunner.Areas.Shops.Views.Models
{
    public class ShowServerShopViewModel : IViewModel
    {
        public IEnumerable<Product> Products { get; internal set; }
    }
}