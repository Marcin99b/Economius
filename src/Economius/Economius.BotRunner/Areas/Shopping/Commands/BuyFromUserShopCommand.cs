using Discord;
using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Shopping.Commands
{
    public class BuyFromUserShopCommand : IBotCommand
    {
        public const string CommandName = "buy-from-user-shop";

        public IUser User { get; set; }
        public const string Param_User = "user";
        public string Product { get; set; }
        public const string Param_Product = "product";

        public static SlashCommandProperties CreateCommandInfo()
        {
            return new SlashCommandBuilder()
                .WithName(CommandName)
                .WithDescription("Buy from user shop.")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_User)
                    .WithDescription("Select user.")
                    .WithType(ApplicationCommandOptionType.User)
                    .WithRequired(true))
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_Product)
                    .WithDescription("Select product.")
                    .WithType(ApplicationCommandOptionType.String)
                    .WithRequired(true))
                .Build();
        }
    }
}
