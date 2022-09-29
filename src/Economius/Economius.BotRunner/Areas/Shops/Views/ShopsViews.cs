using Discord;
using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using Economius.BotRunner.Areas.Payments.Commands;
using Economius.BotRunner.Areas.Shops.Views.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.BotRunner.Areas.Shops.Views
{
    public interface IShopsViews : IViewsService
    {
        Task AddProductToMyShopView(SocketSlashCommand rawCommand, AddProductToMyShopViewModel model);
        Task AddProductToServerShopView(SocketSlashCommand rawCommand, AddProductToServerShopViewModel model);
        Task BuyFromServerShopView(SocketSlashCommand rawCommand, BuyFromServerShopViewModel model);
        Task BuyFromUserShopView(SocketSlashCommand rawCommand, BuyFromUserShopViewModel model);
        Task RemoveProductFromMyShop(SocketSlashCommand rawCommand, RemoveProductFromMyShopViewModel model);
        Task RemoveProductFromServerShopView(SocketSlashCommand rawCommand, RemoveProductFromServerShopViewModel model);
        Task ShowServerShopView(SocketSlashCommand rawCommand, ShowServerShopViewModel model);
        Task ShowUserShopView(SocketSlashCommand rawCommand, ShowUserShopViewModel model);
    }

    public class ShopsViews : IShopsViews
    {
        private readonly IEmbedBuildersFactory embedBuildersFactory;

        public ShopsViews(IEmbedBuildersFactory embedBuildersFactory)
        {
            this.embedBuildersFactory = embedBuildersFactory;
        }

        public Task AddProductToMyShopView(SocketSlashCommand rawCommand, AddProductToMyShopViewModel model)
        {
            var embed = this.embedBuildersFactory
                .CreateDefaultEmbedBuilder()
                .WithTitle("Wallet")
                .WithFields(new[]
                {
                    new EmbedFieldBuilder()
                        .WithName(ShowWalletCommand.Param_User) //todo translation
                        .WithValue(this.GetUserText(model.UserId)).WithIsInline(true),
                    new EmbedFieldBuilder()
                        .WithName("balance")
                        .WithValue(model.Balance).WithIsInline(true),
                })
                .Build();

            return rawCommand.RespondAsync(embed: embed);
        }

        public Task AddProductToServerShopView(SocketSlashCommand rawCommand, AddProductToServerShopViewModel model)
        {
            return Task.CompletedTask;
        }

        public Task BuyFromServerShopView(SocketSlashCommand rawCommand, BuyFromServerShopViewModel model)
        {
            return Task.CompletedTask;
        }

        public Task BuyFromUserShopView(SocketSlashCommand rawCommand, BuyFromUserShopViewModel model)
        {
            return Task.CompletedTask;
        }

        public Task RemoveProductFromMyShop(SocketSlashCommand rawCommand, RemoveProductFromMyShopViewModel model)
        {
            return Task.CompletedTask;
        }

        public Task RemoveProductFromServerShopView(SocketSlashCommand rawCommand, RemoveProductFromServerShopViewModel model)
        {
            return Task.CompletedTask;
        }

        public Task ShowServerShopView(SocketSlashCommand rawCommand, ShowServerShopViewModel model)
        {
            return Task.CompletedTask;
        }

        public Task ShowUserShopView(SocketSlashCommand rawCommand, ShowUserShopViewModel model)
        {
            return Task.CompletedTask;
        }
    }
}
