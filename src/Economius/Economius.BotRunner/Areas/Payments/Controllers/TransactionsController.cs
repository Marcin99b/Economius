using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using Economius.BotRunner.Areas.Payments.Commands;
using Economius.BotRunner.Areas.Payments.Views;
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
            if(fromUserWallet.Balance < transactionCommand.Amount)
            {
                return new ErrorViewModel($"User <@{fromUserId}> has not enough balance in wallet.");
            }

            var command = new CreateTransactionCommand(serverId, fromUserId, toUserId, transactionCommand.Amount, transactionCommand.Comment);
            await this.commandBus.ExecuteAsync(command);

            //todo performance
            await this.commandBus.ExecuteAsync(new RecalculateWalletBalanceCommand(serverId, fromUserId));
            await this.commandBus.ExecuteAsync(new RecalculateWalletBalanceCommand(serverId, toUserId));

            return new TransactionViewModel(fromUserId, toUserId, transactionCommand.Amount, transactionCommand.Comment);
        }

        public async Task<IViewModel> IncreaseServerBalance(SocketSlashCommand rawCommand, IncreaseServerBalanceCommand increaseServerBalanceCommand)
        {
            var serverId = rawCommand.GuildId!.Value;
            var serverWallet = this.queryBus.Execute(new GetWalletQuery(userServerPair: (serverId, 0))).Wallet;
            if (serverWallet.Balance + increaseServerBalanceCommand.Amount < 0)
            {
                return new ErrorViewModel($"Server has not enough balance in wallet.");
            }
            // todo generate by outside template
            var comment = $"Server balance insreased by {increaseServerBalanceCommand.Amount}. Action performed by user <@{rawCommand.User.Id}>.";
            var command = new CreateTransactionCommand(serverId, 0, 0, increaseServerBalanceCommand.Amount, comment);
            await this.commandBus.ExecuteAsync(command);

            await this.commandBus.ExecuteAsync(new RecalculateWalletBalanceCommand(serverId, 0));
            return new TransactionViewModel(0, 0, increaseServerBalanceCommand.Amount, comment);
        }
    }
}
