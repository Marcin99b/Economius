using Discord;
using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Configuration.Commands
{
    public class SetupServerCommand : IBotCommand
    {
        public const string CommandName = "setup-server";
        /*
        public long UserStartMoney { get; set; }
        public const string Param_UserStartMoney = "user-start-money";

        public long ServerStartMoney { get; set; }
        public const string Param_ServerStartMoney = "server-start-money";
        */
        public long IncomeTaxPercentage { get; set; }
        public const string Param_IncomeTaxPercentage = "income-tax-percentage";

        public static SlashCommandProperties CreateCommandInfo()
        {
            return new SlashCommandBuilder()
                .WithName(CommandName)
                .WithDescription("Provide required configuration.")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_IncomeTaxPercentage)
                    .WithDescription("User gives selected % of income to server budget.")
                    .WithAutocomplete(true)
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithMaxValue(100)
                    .WithRequired(true))
                .Build();
        }
    }
}
