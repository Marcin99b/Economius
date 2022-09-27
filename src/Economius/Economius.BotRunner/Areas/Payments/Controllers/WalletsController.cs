using Discord;
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
    public interface IWalletsController
    {
        Task<IViewModel> ShowWallet(SocketSlashCommand rawCommand, ShowWalletCommand showWalletCommand);
    }

    public class WalletsController : IWalletsController
    {
        private readonly IQueryBus queryBus;
        private readonly ICommandBus commandBus;

        public WalletsController(IQueryBus queryBus, ICommandBus commandBus)
        {
            this.queryBus = queryBus;
            this.commandBus = commandBus;
        }

        public Task<IViewModel> ShowWallet(SocketSlashCommand rawCommand, ShowWalletCommand showWalletCommand)
        {
            var userId = showWalletCommand.User?.Id ?? rawCommand.User.Id;
            var query = new GetWalletQuery(userServerPair: (rawCommand.GuildId!.Value, userId));
            var wallet = this.queryBus.Execute(query).Wallet!; //wallet must exist always
            IViewModel result = new ShowWalletViewModel(wallet.UserId, wallet.Balance);
            return Task.FromResult(result);
        }
    }
}
