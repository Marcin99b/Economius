using Discord;
using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Shopping.Commands
{
    public class BuyFromServerShopCommand : IBotCommand
    {
        public const string CommandName = "buy-from-server-shop";

        public string ProductIdentifier { get; set; }
        public const string Param_ProductIdentifier = "product-identifier";
        public string Price { get; set; }
        public const string Param_Price = "product";

        public static SlashCommandProperties CreateCommandInfo()
        {
            return new SlashCommandBuilder()
                .WithName(CommandName)
                .WithDescription("Buy from server shop.")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_ProductIdentifier)
                    .WithDescription("Select product.")
                    .WithType(ApplicationCommandOptionType.String)
                    .WithRequired(true))
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_Price)
                    .WithDescription("Put price to confirm.")
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithRequired(true))
                .Build();
        }
    }
}
