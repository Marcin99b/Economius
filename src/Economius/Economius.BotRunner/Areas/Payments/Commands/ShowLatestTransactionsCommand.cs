using Discord;
using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Payments.Commands
{
    public class ShowLatestTransactionsCommand : IBotCommand
    {
        public const string CommandName = "show-latest-transactions";

        public long Quantity { get; set; }
        public const string Param_Quantity = "quantity";

        public IUser? User { get; set; }
        public const string Param_User = "user";

        public static SlashCommandProperties CreateCommandInfo()
        {
            return new SlashCommandBuilder()
                .WithName(CommandName)
                .WithDescription("Show latest transactions of user.")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_Quantity)
                    .WithDescription("Select quantity.")
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithRequired(true))
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_User)
                    .WithDescription("Select user.")
                    .WithType(ApplicationCommandOptionType.User)
                    .WithRequired(false))
                .Build();
        }
    }
}
