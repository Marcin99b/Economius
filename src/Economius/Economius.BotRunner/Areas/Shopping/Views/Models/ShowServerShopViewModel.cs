using Economius.BotRunner.Areas.Commons;
using Economius.Domain.Shopping;

namespace Economius.BotRunner.Areas.Shopping.Views.Models
{
    public class ShowServerShopViewModel : IViewModel
    {
        public IEnumerable<Product> Products { get; }

        public ShowServerShopViewModel(IEnumerable<Product> products)
        {
            this.Products = products;
        }
    }
}