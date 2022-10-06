using Discord;
using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using Economius.BotRunner.Areas.Payments.Views.Models;

namespace Economius.BotRunner.Areas.Payments.Views
{
    public interface ITransactionsViews : IViewsService
    {
        Task TransactionView(SocketSlashCommand rawCommand, TransactionViewModel model);
        Task TransactionsView(SocketSlashCommand rawCommand, TransactionsViewModel model);
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
                        .WithValue(this.GetUserText(model.FromUserId)).WithIsInline(true),
                    new EmbedFieldBuilder()
                        .WithName("To user")
                        .WithValue(this.GetUserText(model.ToUserId)).WithIsInline(true),
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

        public Task TransactionsView(SocketSlashCommand rawCommand, TransactionsViewModel model)
        {
            var fields = this.GenerateTransactionsAsFields(model.transactionViewModels).ToArray();
            var embed = this.embedBuildersFactory
                .CreateDefaultEmbedBuilder()
                .WithTitle("Transactions")
                .WithFields(fields)
                .Build();

            return rawCommand.RespondAsync(embed: embed);
        }

        private IEnumerable<EmbedFieldBuilder> GenerateTransactionsAsFields(IEnumerable<TransactionViewModel> transactionViewModels)
        {
            byte i = 0;
            foreach (var item in transactionViewModels)
            {
                if(i != 0 && i % 2 == 0)
                {
                    yield return new EmbedFieldBuilder().WithName("\u200b").WithValue("\u200b").WithIsInline(false);
                }
                yield return new EmbedFieldBuilder()
                    .WithName("ID: " + item.TransactionId.ToString())
                    .WithValue(
                        $"From: {this.GetUserText(item.FromUserId)}\n" +
                        $"To: {this.GetUserText(item.ToUserId)}\n" +
                        $"Amount: {item.Amount}\n" +
                        $"Date: {item.CreatedAt}\n" +
                        $"Comment: {item.Comment}")
                    .WithIsInline(true);
                i++;
            }
        }

        private string GetUserText(ulong id)
        {
            if(id == 0)
            {
                return "SERVER";
            }
            return $"<@{id}>";
        }
    }
}
