﻿using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using Economius.BotRunner.Areas.Shopping.Commands;
using Economius.BotRunner.Areas.Shopping.Views.Models;
using Economius.Cqrs;
using Economius.Domain.Shopping.Cqrs;

namespace Economius.BotRunner.Areas.Shopping.Controllers
{
    public interface IShopsController : IController
    {
        /*
        Task<IViewModel> AddProductToMyShop(SocketSlashCommand rawCommand, AddProductToMyShopCommand command);
        Task<IViewModel> AddProductToServerShop(SocketSlashCommand rawCommand, AddProductToServerShopCommand command);
        Task<IViewModel> BuyFromServerShop(SocketSlashCommand rawCommand, BuyFromServerShopCommand command);
        Task<IViewModel> BuyFromUserShop(SocketSlashCommand rawCommand, BuyFromUserShopCommand command);
        Task<IViewModel> RemoveProductFromMyShop(SocketSlashCommand rawCommand, RemoveProductFromMyShopCommand command);
        Task<IViewModel> RemoveProductFromServerShop(SocketSlashCommand rawCommand, RemoveProductFromServerShopCommand command);
        */
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
        /*
        public async Task<IViewModel> AddProductToMyShop(SocketSlashCommand rawCommand, AddProductToMyShopCommand command)
        {
            await Task.CompletedTask;
            return new AddProductToMyShopViewModel();
        }

        public async Task<IViewModel> AddProductToServerShop(SocketSlashCommand rawCommand, AddProductToServerShopCommand command)
        {
            await Task.CompletedTask;
            return new AddProductToServerShopViewModel();
        }

        public async Task<IViewModel> BuyFromServerShop(SocketSlashCommand rawCommand, BuyFromServerShopCommand command)
        {
            await Task.CompletedTask;
            return new BuyFromServerShopViewModel();
        }

        public async Task<IViewModel> BuyFromUserShop(SocketSlashCommand rawCommand, BuyFromUserShopCommand command)
        {
            await Task.CompletedTask;
            return new BuyFromUserShopViewModel();
        }

        public async Task<IViewModel> RemoveProductFromMyShop(SocketSlashCommand rawCommand, RemoveProductFromMyShopCommand command)
        {
            await Task.CompletedTask;
            return new RemoveProductFromMyShopViewModel();
        }

        public async Task<IViewModel> RemoveProductFromServerShop(SocketSlashCommand rawCommand, RemoveProductFromServerShopCommand command)
        {
            await Task.CompletedTask;
            return new RemoveProductFromServerShopViewModel();
        }
        */
        public Task<IViewModel> ShowServerShop(SocketSlashCommand rawCommand, ShowServerShopCommand command)
        {
            var shop = this.queryBus.Execute(new GetShopQuery(rawCommand.GuildId!.Value, 0)).Shop!;
            return Task.FromResult(new ShowServerShopViewModel(shop.Products) as IViewModel);
        }

        public Task<IViewModel> ShowUserShop(SocketSlashCommand rawCommand, ShowUserShopCommand command)
        {
            var userId = command.User.Id;
            var shop = this.queryBus.Execute(new GetShopQuery(rawCommand.GuildId!.Value, userId)).Shop!;
            return Task.FromResult(new ShowUserShopViewModel(userId, shop.Products) as IViewModel);
        }
    }
}