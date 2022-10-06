using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using Economius.BotRunner.Areas.Payments.Commands;
using Economius.BotRunner.Areas.Payments.Views;
using Economius.BotRunner.Areas.Payments.Views.Models;
using Economius.Cqrs;
using Economius.Domain.Payments.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.BotRunner.Areas.Payments.Controllers
{
    public interface ITransactionsController : IController
    {
        Task<IViewModel> Transaction(SocketSlashCommand rawCommand, TransactionCommand transactionCommand);
        Task<IViewModel> IncreaseServerBalance(SocketSlashCommand rawCommand, IncreaseServerBalanceCommand increaseServerBalanceCommand);
    }

    public class TransactionsController : ITransactionsController
    {
        private readonly IQueryBus queryBus;
        private readonly ICommandBus commandBus;

        public TransactionsController(IQueryBus queryBus, ICommandBus commandBus)
        {
            this.queryBus = queryBus;
            this.commandBus = commandBus;
        }

        public async Task<IViewModel> Transaction(SocketSlashCommand rawCommand, TransactionCommand transactionCommand)
        {
            var serverId = rawCommand.GuildId!.Value;
            var fromUserId = rawCommand.User.Id;
            var toUserId = transactionCommand.ToUser.Id;

            var fromUserWallet = this.queryBus.Execute(new GetWalletQuery(userServerPair: (serverId, fromUserId))).Wallet;
            if(fromUserWallet!.Balance < transactionCommand.Amount)
            {
                return new ErrorViewModel($"User <@{fromUserId}> has not enough balance in wallet.");
            }

            var command = new CreateTransactionCommand(serverId, fromUserId, toUserId, transactionCommand.Amount, transactionCommand.Comment);
            await this.commandBus.ExecuteAsync(command);

            //todo performance
            await this.commandBus.ExecuteAsync(new RecalculateWalletBalanceCommand(serverId, fromUserId));
            await this.commandBus.ExecuteAsync(new RecalculateWalletBalanceCommand(serverId, toUserId));

            return new TransactionViewModel(default, fromUserId, toUserId, transactionCommand.Amount, transactionCommand.Comment, default);
        }

        public async Task<IViewModel> TransactionFromServerCommand(SocketSlashCommand rawCommand, TransactionFromServerCommand transactionFromServerCommand)
        {
            var serverId = rawCommand.GuildId!.Value;
            var toUserId = transactionFromServerCommand.ToUser.Id;

            var fromUserWallet = this.queryBus.Execute(new GetWalletQuery(userServerPair: (serverId, 0))).Wallet;
            if (fromUserWallet!.Balance < transactionFromServerCommand.Amount)
            {
                return new ErrorViewModel($"Server has not enough balance in wallet.");
            }

            // todo generate by outside template
            var comment = $"{transactionFromServerCommand.Comment}\n\nAction performed by user <@{rawCommand.User.Id}>.";
            var command = new CreateTransactionCommand(serverId, 0, toUserId, transactionFromServerCommand.Amount, comment);
            await this.commandBus.ExecuteAsync(command);

            //todo performance
            await this.commandBus.ExecuteAsync(new RecalculateWalletBalanceCommand(serverId, 0));
            await this.commandBus.ExecuteAsync(new RecalculateWalletBalanceCommand(serverId, toUserId));

            return new TransactionViewModel(default, 0, toUserId, transactionFromServerCommand.Amount, comment, default);
        }

        public async Task<IViewModel> IncreaseServerBalance(SocketSlashCommand rawCommand, IncreaseServerBalanceCommand increaseServerBalanceCommand)
        {
            var serverId = rawCommand.GuildId!.Value;
            var serverWallet = this.queryBus.Execute(new GetWalletQuery(userServerPair: (serverId, 0))).Wallet;
            if (serverWallet!.Balance + increaseServerBalanceCommand.Amount < 0)
            {
                return new ErrorViewModel($"Server has not enough balance in wallet.");
            }
            // todo generate by outside template
            var comment = $"Server balance insreased by {increaseServerBalanceCommand.Amount}. Action performed by user <@{rawCommand.User.Id}>.";
            var command = new CreateTransactionCommand(serverId, 0, 0, increaseServerBalanceCommand.Amount, comment);
            await this.commandBus.ExecuteAsync(command);

            await this.commandBus.ExecuteAsync(new RecalculateWalletBalanceCommand(serverId, 0));

            return new TransactionViewModel(default, 0, 0, increaseServerBalanceCommand.Amount, comment, default);
        }

        public async Task<IViewModel> ShowLatestTransactions(SocketSlashCommand rawCommand, ShowLatestTransactionsCommand command)
        {
            if(command.Quantity <= 0)
            {
                return new ErrorViewModel("Quantity must be at least 1.");
            }
            if(command.Quantity > 10)
            {
                return new ErrorViewModel("Max quantity is 10.");
            }
            var userId = command.User?.Id ?? rawCommand.User.Id;
            var queryResult = await this.queryBus.ExecuteAsync(new GetTransactionsQuery(rawCommand.GuildId!.Value, userId, Convert.ToInt32(command.Quantity)));
            var transactions = queryResult.Transactions;

            return new TransactionsViewModel(transactions.Select(x => new TransactionViewModel(x.Id, x.FromUserId, x.ToUserId, x.Amount, x.Comment, x.CreatedAt)));
        }
    }
}
