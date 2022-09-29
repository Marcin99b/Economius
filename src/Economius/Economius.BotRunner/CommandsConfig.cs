using Discord;
using Economius.BotRunner.Areas.Configuration.Commands;
using Economius.BotRunner.Areas.Payments.Commands;

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
        };
    }
}
