using Discord;
using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using Economius.BotRunner.Areas.Payments.Commands;
using Economius.BotRunner.Areas.Shopping.Views.Models;
using Economius.Domain.Shopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.BotRunner.Areas.Shopping.Views
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
                .WithTitle("Product added to your shop")
                .WithFields(new[]
                {
                    new EmbedFieldBuilder()
                        .WithName("name") //todo translation
                        .WithValue(model.Name).WithIsInline(true),
                    new EmbedFieldBuilder()
                        .WithName("price")
                        .WithValue(model.Price).WithIsInline(true),
                })
                .Build();

            return rawCommand.RespondAsync(embed: embed);
        }

        public Task AddProductToServerShopView(SocketSlashCommand rawCommand, AddProductToServerShopViewModel model)
        {
            var embed = this.embedBuildersFactory
                .CreateDefaultEmbedBuilder()
                .WithTitle("Product added to server shop")
                .WithFields(new[]
                {
                    new EmbedFieldBuilder()
                        .WithName("name") //todo translation
                        .WithValue(model.Name).WithIsInline(true),
                    new EmbedFieldBuilder()
                        .WithName("price")
                        .WithValue(model.Price).WithIsInline(true),
                })
                .Build();

            return rawCommand.RespondAsync(embed: embed);
        }

        public Task BuyFromServerShopView(SocketSlashCommand rawCommand, BuyFromServerShopViewModel model)
        {
            var embed = this.embedBuildersFactory
                .CreateDefaultEmbedBuilder()
                .WithTitle("Product bought from server shop")
                .WithFields(new[]
                {
                    new EmbedFieldBuilder()
                        .WithName("name") //todo translation
                        .WithValue(model.Name).WithIsInline(true),
                    new EmbedFieldBuilder()
                        .WithName("price")
                        .WithValue(model.Price).WithIsInline(true),
                })
                .Build();

            return rawCommand.RespondAsync(embed: embed);
        }

        public Task BuyFromUserShopView(SocketSlashCommand rawCommand, BuyFromUserShopViewModel model)
        {
            var embed = this.embedBuildersFactory
                .CreateDefaultEmbedBuilder()
                .WithTitle($"Product bought from {this.GetUserText(model.ShopOwnerId)} shop")
                .WithFields(new[]
                {
                    new EmbedFieldBuilder()
                        .WithName("name") //todo translation
                        .WithValue(model.Name).WithIsInline(true),
                    new EmbedFieldBuilder()
                        .WithName("price")
                        .WithValue(model.Price).WithIsInline(true),
                })
                .Build();

            return rawCommand.RespondAsync(embed: embed);
        }

        public Task RemoveProductFromMyShop(SocketSlashCommand rawCommand, RemoveProductFromMyShopViewModel model)
        {
            var embed = this.embedBuildersFactory
                .CreateDefaultEmbedBuilder()
                .WithTitle("Product removed from your shop")
                .WithFields(new[]
                {
                    new EmbedFieldBuilder()
                        .WithName("name") //todo translation
                        .WithValue(model.Name).WithIsInline(true)
                })
                .Build();

            return rawCommand.RespondAsync(embed: embed);
        }

        public Task RemoveProductFromServerShopView(SocketSlashCommand rawCommand, RemoveProductFromServerShopViewModel model)
        {
            var embed = this.embedBuildersFactory
                .CreateDefaultEmbedBuilder()
                .WithTitle("Product removed from server shop")
                .WithFields(new[]
                {
                    new EmbedFieldBuilder()
                        .WithName("name") //todo translation
                        .WithValue(model.Name).WithIsInline(true)
                })
                .Build();

            return rawCommand.RespondAsync(embed: embed);
        }

        public Task ShowServerShopView(SocketSlashCommand rawCommand, ShowServerShopViewModel model)
        {
            var embed = this.embedBuildersFactory
                .CreateDefaultEmbedBuilder()
                .WithTitle("Server shop")
                .WithFields(this.MapProductsToFields(model.Products))
                .Build();

            return rawCommand.RespondAsync(embed: embed);
        }

        public Task ShowUserShopView(SocketSlashCommand rawCommand, ShowUserShopViewModel model)
        {
            var embed = this.embedBuildersFactory
                .CreateDefaultEmbedBuilder()
                .WithTitle($"{this.GetUserText(model.ShopOwnerId)} shop")
                .WithFields(this.MapProductsToFields(model.Products))
                .Build();

            return rawCommand.RespondAsync(embed: embed);
        }

        private EmbedFieldBuilder[] MapProductsToFields(IEnumerable<Product> products)
        {
            return products.Select(x => 
                new EmbedFieldBuilder()
                    .WithName(x.Name) //todo translation
                    .WithValue($"Price: {x.Price}").WithIsInline(false))
            .ToArray();
        }

        private string GetUserText(ulong id)
        {
            if (id == 0)
            {
                return "SERVER";
            }
            return $"<@{id}>";
        }
    }
}
