using Discord;

namespace Economius.BotRunner.Areas.Configuration.Commands
{
    public class SetupServerCommand
    {
        public const string CommandName = "setup-server";

        public long UserStartMoney { get; set; }
        public const string Param_UserStartMoney = "user-start-money";

        public long ServerStartMoney { get; set; }
        public const string Param_ServerStartMoney = "server-start-money";

        public long IncomeTaxPercentage { get; set; }
        public const string Param_IncomeTaxPercentage = "income-tax-percentage";

        public static SlashCommandProperties CreateCommandInfo()
        {
            return new SlashCommandBuilder()
                .WithName(CommandName)
                .WithDescription("Provide required configuration.")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_UserStartMoney)
                    .WithDescription("Amount of coins that has each user on start. We recommend 0 to good balance.")
                    .WithAutocomplete(true)
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithRequired(true))
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_ServerStartMoney)
                    .WithDescription("Amount of coins that has server on start. Set -1 to disable server budget mechanism.")
                    .WithAutocomplete(true)
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithRequired(true))
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_IncomeTaxPercentage)
                    .WithDescription("Set -1 if you disabled server-start-money.")
                    .WithAutocomplete(true)
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithMaxValue(100)
                    .WithRequired(true))
                .Build();
        }
    }
}
