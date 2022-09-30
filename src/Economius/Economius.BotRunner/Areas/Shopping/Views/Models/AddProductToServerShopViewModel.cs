using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Shopping.Views.Models
{
    public class AddProductToServerShopViewModel : IViewModel
    {
        public string Identifier { get; }
        public string Description { get; }
        public long Price { get; }

        public AddProductToServerShopViewModel(string identifier, string description, long price)
        {
            this.Identifier = identifier;
            this.Description = description;
            this.Price = price;
        }
    }
}