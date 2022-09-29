using Discord;
using Economius.BotRunner.Areas.Commons;

namespace Economius.BotRunner.Areas.Shopping.Commands
{
    public class RemoveProductFromServerShopCommand : IBotCommand
    {
        public const string CommandName = "remove-product-from-server-shop";

        public string Name { get; set; }
        public const string Param_Name = "name";

        public static SlashCommandProperties CreateCommandInfo()
        {
            return new SlashCommandBuilder()
                .WithName(CommandName)
                .WithDescription("Add product to my shop.")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName(Param_Name)
                    .WithDescription("Product name.")
                    .WithType(ApplicationCommandOptionType.String)
                    .WithRequired(true))
                .Build();
        }
    }
}
