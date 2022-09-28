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
        public SlashCommandProperties[] CommandsInfos { get; } = new[]
        {
            //Configuration
            SetupServerCommand.CreateCommandInfo(),
            ShowServerSetupCommand.CreateCommandInfo(),
            //Wallets
            ShowWalletCommand.CreateCommandInfo()
        };
    }
}
