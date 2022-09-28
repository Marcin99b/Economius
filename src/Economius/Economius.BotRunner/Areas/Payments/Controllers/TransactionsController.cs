﻿using Discord.WebSocket;
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
    }
}
