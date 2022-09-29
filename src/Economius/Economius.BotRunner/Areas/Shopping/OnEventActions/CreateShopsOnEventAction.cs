using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using Economius.BotRunner.Areas.Payments.OnEventActions;
using Economius.Cqrs;
using Economius.Domain.Payments;
using Economius.Domain.Payments.Cqrs;
using Economius.Domain.Shopping.Cqrs;

namespace Economius.BotRunner.Areas.Shopping.OnEventActions
{
    public interface ICreateShopsOnEventAction : IOnEventAction
    {
        Task Run(DiscordSocketClient client);
        Task Run(DiscordSocketClient client, SocketGuild guild);
        Task Run(DiscordSocketClient client, ulong serverId, ulong userId);
    }

    public class CreateShopsOnEventAction : ICreateWalletsOnEventAction
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryBus queryBus;

        public CreateShopsOnEventAction(ICommandBus commandBus, IQueryBus queryBus)
        {
            this.commandBus = commandBus;
            this.queryBus = queryBus;
        }

        public IOnEventActionOrder Order => IOnEventActionOrder.AfterFirstGroup;

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
            await this.Run(client, guild.Id, 0);//server shop

            var users = await guild.GetUsersAsync().FlattenAsync();
            foreach (var user in users)
            {
                await this.Run(client, guild.Id, user.Id);
            }
        }

        public async Task Run(DiscordSocketClient client, ulong serverId, ulong userId)
        {
            var foundShop = this.queryBus.Execute(new GetShopQuery(serverId, userId)).Shop;
            if(foundShop != null)
            {
                return;
            }
            int i = 0;
            Wallet? wallet = null;
            while (wallet == null)
            {
                try
                {
                    wallet = this.queryBus.Execute(new GetWalletQuery(userServerPair: (serverId, userId))).Wallet;
                }
                catch (Exception ex)
                {
                    if (i >= 3)
                    {
                        throw new Exception($"Wallet for server {serverId} user {userId} is not generated. Cannot create shop.", ex);
                    }
                    Thread.Sleep(1000);
                }
            }
            var command = new CreateShopCommand(wallet.Id, serverId, userId);
            await this.commandBus.AddToSingleThreadQueue(command);
        }
    }
}
