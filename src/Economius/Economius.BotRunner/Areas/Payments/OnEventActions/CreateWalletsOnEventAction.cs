using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using Economius.Cqrs;
using Economius.Domain.Configurations;
using Economius.Domain.Configurations.Cqrs;
using Economius.Domain.Payments.Cqrs;
using MongoDB.Driver.Core.Servers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.BotRunner.Areas.Payments.OnEventActions
{
    public interface ICreateWalletsOnEventAction : IOnEventAction
    {
        Task Run(DiscordSocketClient client);
        Task Run(DiscordSocketClient client, SocketGuild guild);
        Task Run(DiscordSocketClient client, ulong serverId, ulong userId);
    }

    public class CreateWalletsOnEventAction : ICreateWalletsOnEventAction
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryBus queryBus;

        public CreateWalletsOnEventAction(ICommandBus commandBus, IQueryBus queryBus)
        {
            this.commandBus = commandBus;
            this.queryBus = queryBus;
        }

        public IOnEventActionOrder Order => IOnEventActionOrder.Immediate;

        //todo run after setup
        //todo run after command
        public void Configure(DiscordSocketClient client)
        {
            client.JoinedGuild += guild => this.Run(client, guild);
            client.GuildAvailable += guild => this.Run(client, guild);
            client.UserJoined += user => this.Run(client, user.Guild.Id, user.Id);
            client.Ready += () => this.Run(client);
        }

        //todo performance
        public async Task Run(DiscordSocketClient client)
        {
            foreach (var guild in client.Guilds)
            {
                await this.Run(client, guild);
            }
        }

        public async Task Run(DiscordSocketClient client, SocketGuild guild)
        {
            //setup server wallet
            await this.Run(client, guild.Id, 0);

            var users = await guild.GetUsersAsync().FlattenAsync();
            foreach (var user in users)
            {
                await this.Run(client, guild.Id, user.Id);
            }
        }

        public async Task Run(DiscordSocketClient client, ulong serverId, ulong userId)
        {
            var foundWallet = this.queryBus.Execute(new GetWalletQuery(userServerPair: (serverId, userId))).Wallet;
            if(foundWallet != null)
            {
                return;
            }
            var command = new CreateWalletCommand(serverId, userId);
            await this.commandBus.AddToSingleThreadQueue(command);
        }
    }
}
