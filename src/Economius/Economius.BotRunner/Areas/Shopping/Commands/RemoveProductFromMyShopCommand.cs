using Discord;
using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Shopping.Commands
{
    public class RemoveProductFromMyShopCommand : IBotCommand
    {
        public const string CommandName = "remove-product-from-my-shop";

        public string Identifier { get; set; }
        public const string Param_Identifier = "identifier";

        public static SlashCommandProperties CreateCommandInfo()
        {
            return new SlashCommandBuilder()
                .WithName(CommandName)
                .WithDescription("Add product to my shop.")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_Identifier)
                    .WithDescription("Product identifier.")
                    .WithType(ApplicationCommandOptionType.String)
                    .WithRequired(true))
                .Build();
        }
    }
}
