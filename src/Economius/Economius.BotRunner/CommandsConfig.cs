using Discord;
using Economius.BotRunner.Areas.Configuration.Commands;
using Economius.BotRunner.Areas.Payments.Commands;
using Economius.BotRunner.Areas.Shopping.Commands;

namespace Economius.BotRunner
{
    public interface ICommandsConfig
    {
        SlashCommandProperties[] CommandsInfos { get; }
    }

    public class CommandsConfig : ICommandsConfig
    {
        //todo reflection
        public SlashCommandProperties[] CommandsInfos { get; } = new[]
        {
            //Configuration
            SetupServerCommand.CreateCommandInfo(),
            TransactionFromServerCommand.CreateCommandInfo(),
            ShowServerSetupCommand.CreateCommandInfo(),
            //Wallets
            ShowWalletCommand.CreateCommandInfo(),
            ShowServerWalletCommand.CreateCommandInfo(),
            //Transactions
            TransactionCommand.CreateCommandInfo(),
            IncreaseServerBalanceCommand.CreateCommandInfo(),
            //Shops
            AddProductToMyShopCommand.CreateCommandInfo(),
            AddProductToServerShopCommand.CreateCommandInfo(),
            BuyFromServerShopCommand.CreateCommandInfo(),
            BuyFromUserShopCommand.CreateCommandInfo(),
            RemoveProductFromMyShopCommand.CreateCommandInfo(),
            RemoveProductFromServerShopCommand.CreateCommandInfo(),
            ShowServerShopCommand.CreateCommandInfo(),
            ShowUserShopCommand.CreateCommandInfo(),
        };
    }
}
