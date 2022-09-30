using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Shopping.Views.Models
{
    public class RemoveProductFromServerShopViewModel : IViewModel
    {
        public string Identifier { get; }

        public RemoveProductFromServerShopViewModel(string identifier)
        {
            this.Identifier = identifier;
        }
    }
}