using Discord;
using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Shops.Commands
{
    public class AddProductToServerShopCommand : IBotCommand
    {
        public const string CommandName = "add-product-to-server-shop";

        public string Name { get; set; }
        public const string Param_Name = "name";
        public long Price { get; set; }
        public const string Param_Price = "price";

        public static SlashCommandProperties CreateCommandInfo()
        {
            return new SlashCommandBuilder()
                .WithName(CommandName)
                .WithDescription("Add product to server shop.")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_Name)
                    .WithDescription("Product name.")
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
