using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Economius.BotRunner
{
    public class Program
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private DiscordSocketClient _client;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public static Task Main(string[] args) => new Program().MainAsync(args);

        public async Task MainAsync(string[] args)
        {
            var token = args[0];
            _client = new DiscordSocketClient();

            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            _client.Ready += Client_Ready;
            _client.SlashCommandExecuted += SlashCommandHandler;

            _client.ModalSubmitted += async modal =>
            {
                // Get the values of components.
                List<SocketMessageComponentData> components =
                    modal.Data.Components.ToList();
                string food = components
                    .First(x => x.CustomId == "food_name").Value;
                string reason = components
                    .First(x => x.CustomId == "food_reason").Value;

                // Build the message to send.
                string message = "hey @everyone; I just learned " +
                    $"{modal.User.Mention}'s favorite food is " +
                    $"{food} because {reason}.";

                // Specify the AllowedMentions so we don't actually ping everyone.
                AllowedMentions mentions = new AllowedMentions();
                mentions.AllowedTypes = AllowedMentionTypes.Users;

                // Respond to the modal.
                await modal.RespondAsync(message, allowedMentions: mentions);
            };

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task Client_Ready()
        {

            var helloWorld = new SlashCommandBuilder()
                .WithName("hello-world")
                .WithDescription("Print hello world text");

            var showMyText = new SlashCommandBuilder()
                .WithName("print-my-text")
                .WithDescription("Print text from argument")
                .AddOption("text", ApplicationCommandOptionType.String, "Text that will be printed", isRequired: true);

            try
            {
                await _client.BulkOverwriteGlobalApplicationCommandsAsync(new[] { helloWorld.Build(), showMyText.Build() });
            }
            catch (HttpException exception)
            {
                var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
                Console.WriteLine(json);
            }
        }

        private async Task SlashCommandHandler(SocketSlashCommand command)
        {
            var json = JsonConvert.SerializeObject(command.Data, Formatting.Indented);
            //await command.RespondAsync($"```\n{json}\n```");
            var mb = new ModalBuilder()
                .WithTitle("Fav Food")
                .WithCustomId("food_menu")
                .AddTextInput("What??", "food_name", placeholder: "Pizza")
                .AddTextInput("Why??", "food_reason", TextInputStyle.Paragraph,
                    "Kus it's so tasty");
            await command.RespondWithModalAsync(mb.Build());
        }
    }
}
