using Discord;
using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Payments.Commands
{
    public class TransactionCommand : IBotCommand
    {
        public const string CommandName = "transaction";

        public IUser ToUser { get; set; }
        public const string Param_ToUser = "to-user";
        public long Amount { get; set; }
        public const string Param_Amount = "amount";
        public string Comment { get; set; }
        public const string Param_Comment = "comment";

        public static SlashCommandProperties CreateCommandInfo()
        {
            return new SlashCommandBuilder()
                .WithName(CommandName)
                .WithDescription("Make transaction from you to another user.")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_ToUser)
                    .WithDescription("Select user.")
                    .WithType(ApplicationCommandOptionType.User)
                    .WithRequired(true))
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_Amount)
                    .WithDescription("Select amount.")
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithRequired(true))
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_Comment)
                    .WithDescription("Add comment.")
                    .WithType(ApplicationCommandOptionType.String)
                    .WithRequired(true))
                .Build();
        }
    }
}
