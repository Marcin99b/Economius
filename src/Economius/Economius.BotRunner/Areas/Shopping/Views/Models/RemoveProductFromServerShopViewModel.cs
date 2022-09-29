using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Shopping.Views.Models
{
    public class RemoveProductFromServerShopViewModel : IViewModel
    {
        public string Name { get; }

        public RemoveProductFromServerShopViewModel(string name)
        {
            this.Name = name;
        }
    }
}