using Discord;
using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Payments.Commands
{
    public class IncreaseServerBalanceCommand : IBotCommand
    {
        public const string CommandName = "increase-server-balance";

        public long Amount { get; set; }
        public const string Param_Amount = "amount";

        public static SlashCommandProperties CreateCommandInfo()
        {
            return new SlashCommandBuilder()
                .WithName(CommandName)
                .WithDescription("Increase server balance. There is possiblity to decrease by negative number.")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_Amount)
                    .WithDescription("Select amount.")
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithRequired(true))
                .Build();
        }
    }
}
