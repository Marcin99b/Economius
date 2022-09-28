using Discord;
using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using Economius.BotRunner.Areas.Payments.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.BotRunner.Areas.Payments.Views
{
    public interface IWalletsViews : IViewsService
    {
        Task ShowWalletView(SocketSlashCommand rawCommand, ShowWalletViewModel model);
    }

    public class WalletsViews : IWalletsViews
    {
        private readonly IEmbedBuildersFactory embedBuildersFactory;

        public WalletsViews(IEmbedBuildersFactory embedBuildersFactory)
        {
            this.embedBuildersFactory = embedBuildersFactory;
        }

        public Task ShowWalletView(SocketSlashCommand rawCommand, ShowWalletViewModel model)
        {
            var embed = this.embedBuildersFactory
                .CreateDefaultEmbedBuilder()
                .WithTitle("Wallet")
                .WithFields(new[]
                {
                    new EmbedFieldBuilder()
                        .WithName(ShowWalletCommand.Param_User) //todo translation
                        .WithValue($"<@{model.UserId}>").WithIsInline(true),
                    new EmbedFieldBuilder()
                        .WithName("balance")
                        .WithValue(model.Balance).WithIsInline(true),
                })
                .Build();

            return rawCommand.RespondAsync(embed: embed);
        }
    }
}
