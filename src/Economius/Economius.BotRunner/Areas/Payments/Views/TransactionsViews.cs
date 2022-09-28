using Discord;
using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Payments.Views
{
    public interface ITransactionsViews : IViewsService
    {
        Task TransactionView(SocketSlashCommand rawCommand, TransactionViewModel model);
    }

    public class TransactionsViews : ITransactionsViews
    {
        private readonly IEmbedBuildersFactory embedBuildersFactory;

        public TransactionsViews(IEmbedBuildersFactory embedBuildersFactory)
        {
            this.embedBuildersFactory = embedBuildersFactory;
        }

        public Task TransactionView(SocketSlashCommand rawCommand, TransactionViewModel model)
        {
            var embed = this.embedBuildersFactory
                .CreateDefaultEmbedBuilder()
                .WithTitle("Transaction")
                .WithFields(new[]
                {
                    new EmbedFieldBuilder()
                        .WithName("From user") //todo translation
                        .WithValue($"<@{model.FromUserId}>").WithIsInline(true),
                    new EmbedFieldBuilder()
                        .WithName("To user")
                        .WithValue($"<@{model.ToUserId}>").WithIsInline(true),
                    new EmbedFieldBuilder()
                        .WithName("Amount")
                        .WithValue(model.Amount).WithIsInline(false),
                    new EmbedFieldBuilder()
                        .WithName("Comment")
                        .WithValue(model.Comment).WithIsInline(false),
                })
                .Build();

            return rawCommand.RespondAsync(embed: embed);
        }
    }
}
