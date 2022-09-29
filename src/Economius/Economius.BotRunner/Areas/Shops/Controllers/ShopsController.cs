using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using Economius.BotRunner.Areas.Shops.Commands;

namespace Economius.BotRunner.Areas.Shops.Controllers
{
    public interface IShopsController : IController
    {
        Task<IViewModel> AddProductToMyShop(SocketSlashCommand rawCommand, AddProductToMyShopCommand command);
        Task<IViewModel> AddProductToServerShop(SocketSlashCommand rawCommand, AddProductToServerShopCommand command);
        Task<IViewModel> BuyFromServerShop(SocketSlashCommand rawCommand, BuyFromServerShopCommand command);
        Task<IViewModel> BuyFromUserShop(SocketSlashCommand rawCommand, BuyFromUserShopCommand command);
        Task<IViewModel> RemoveProductFromMyShop(SocketSlashCommand rawCommand, RemoveProductFromMyShopCommand command);
        Task<IViewModel> RemoveProductFromServerShop(SocketSlashCommand rawCommand, RemoveProductFromServerShopCommand command);
        Task<IViewModel> ShowServerShop(SocketSlashCommand rawCommand, ShowServerShopCommand command);
        Task<IViewModel> ShowUserShop(SocketSlashCommand rawCommand, ShowUserShopCommand command);
    }

    public class ShopsController : IShopsController
    {
        public Task<IViewModel> AddProductToMyShop(SocketSlashCommand rawCommand, AddProductToMyShopCommand command)
        {
            return null;
        }

        public Task<IViewModel> AddProductToServerShop(SocketSlashCommand rawCommand, AddProductToServerShopCommand command)
        {
            return null;
        }

        public Task<IViewModel> BuyFromServerShop(SocketSlashCommand rawCommand, BuyFromServerShopCommand command)
        {
            return null;
        }

        public Task<IViewModel> BuyFromUserShop(SocketSlashCommand rawCommand, BuyFromUserShopCommand command)
        {
            return null;
        }

        public Task<IViewModel> RemoveProductFromMyShop(SocketSlashCommand rawCommand, RemoveProductFromMyShopCommand command)
        {
            return null;
        }

        public Task<IViewModel> RemoveProductFromServerShop(SocketSlashCommand rawCommand, RemoveProductFromServerShopCommand command)
        {
            return null;
        }

        public Task<IViewModel> ShowServerShop(SocketSlashCommand rawCommand, ShowServerShopCommand command)
        {
            return null;
        }

        public Task<IViewModel> ShowUserShop(SocketSlashCommand rawCommand, ShowUserShopCommand command)
        {
            return null;
        }
    }
}
