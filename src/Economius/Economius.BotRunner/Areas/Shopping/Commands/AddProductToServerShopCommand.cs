using Discord;
using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Shopping.Commands
{
    public class AddProductToServerShopCommand : IBotCommand
    {
        public const string CommandName = "add-product-to-server-shop";

        public string Identifier { get; set; }
        public const string Param_Identifier = "identifier";
        public string Description { get; set; }
        public const string Param_Description = "description";
        public long Price { get; set; }
        public const string Param_Price = "price";

        public static SlashCommandProperties CreateCommandInfo()
        {
            return new SlashCommandBuilder()
                .WithName(CommandName)
                .WithDescription("Add product to server shop.")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_Identifier)
                    .WithDescription("Product identifier.")
                    .WithType(ApplicationCommandOptionType.String)
                    .WithRequired(true))
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_Description)
                    .WithDescription("Product description.")
                    .WithType(ApplicationCommandOptionType.String)
                    .WithRequired(true))
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_Price)
                    .WithDescription("Product price.")
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithRequired(true))
                .Build();
        }
    }
}
