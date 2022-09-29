using Discord;
using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Shopping.Commands
{
    public class BuyFromServerShopCommand : IBotCommand
    {
        public const string CommandName = "buy-from-server-shop";

        public string Product { get; set; }
        public const string Param_Product = "product";

        public static SlashCommandProperties CreateCommandInfo()
        {
            return new SlashCommandBuilder()
                .WithName(CommandName)
                .WithDescription("Buy from server shop.")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_Product)
                    .WithDescription("Select product.")
                    .WithType(ApplicationCommandOptionType.String)
                    .WithRequired(true))
                .Build();
        }
    }
}
