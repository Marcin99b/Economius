using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using Economius.BotRunner.Areas.Shopping.Commands;
using Economius.Cqrs;
using Economius.Domain.Shopping.Cqrs;

namespace Economius.BotRunner.Areas.Shopping.Controllers
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
        private readonly IQueryBus queryBus;
        private readonly ICommandBus commandBus;

        public ShopsController(IQueryBus queryBus, ICommandBus commandBus)
        {
            this.queryBus = queryBus;
            this.commandBus = commandBus;
        }

        public async Task<IViewModel> AddProductToMyShop(SocketSlashCommand rawCommand, AddProductToMyShopCommand command)
        {
            await Task.CompletedTask;
            return new ErrorViewModel("Command not implemented.");
        }

        public async Task<IViewModel> AddProductToServerShop(SocketSlashCommand rawCommand, AddProductToServerShopCommand command)
        {
            await Task.CompletedTask;
            return new ErrorViewModel("Command not implemented.");
        }

        public async Task<IViewModel> BuyFromServerShop(SocketSlashCommand rawCommand, BuyFromServerShopCommand command)
        {
            await Task.CompletedTask;
            return new ErrorViewModel("Command not implemented.");
        }

        public async Task<IViewModel> BuyFromUserShop(SocketSlashCommand rawCommand, BuyFromUserShopCommand command)
        {
            await Task.CompletedTask;
            return new ErrorViewModel("Command not implemented.");
        }

        public async Task<IViewModel> RemoveProductFromMyShop(SocketSlashCommand rawCommand, RemoveProductFromMyShopCommand command)
        {
            await Task.CompletedTask;
            return new ErrorViewModel("Command not implemented.");
        }

        public async Task<IViewModel> RemoveProductFromServerShop(SocketSlashCommand rawCommand, RemoveProductFromServerShopCommand command)
        {
            await Task.CompletedTask;
            return new ErrorViewModel("Command not implemented.");
        }

        public async Task<IViewModel> ShowServerShop(SocketSlashCommand rawCommand, ShowServerShopCommand command)
        {
            var shop = this.queryBus.Execute(new GetShopQuery(rawCommand.GuildId!.Value, 0)).Shop;
            await Task.CompletedTask;
            return new ErrorViewModel("Command not implemented.");
        }

        public async Task<IViewModel> ShowUserShop(SocketSlashCommand rawCommand, ShowUserShopCommand command)
        {
            await Task.CompletedTask;
            return new ErrorViewModel("Command not implemented.");
        }
    }
}
