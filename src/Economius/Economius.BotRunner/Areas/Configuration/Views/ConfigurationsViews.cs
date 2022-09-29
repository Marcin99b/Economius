using Discord;
using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using Economius.BotRunner.Areas.Configuration.Commands;
using Economius.BotRunner.Areas.Configuration.Views.Models;
using Economius.Domain.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.BotRunner.Areas.Configuration.Views
{
    public interface IConfigurationsViews : IViewsService
    {
        Task ShowServerSetupView(SocketSlashCommand rawCommand, ShowServerSetupViewModel model);
    }

    public class ConfigurationsViews : IConfigurationsViews
    {
        private readonly IEmbedBuildersFactory embedBuildersFactory;

        public ConfigurationsViews(IEmbedBuildersFactory embedBuildersFactory)
        {
            this.embedBuildersFactory = embedBuildersFactory;
        }

        public Task ShowServerSetupView(SocketSlashCommand rawCommand, ShowServerSetupViewModel model)
        {
            var embed = this.embedBuildersFactory
                .CreateDefaultEmbedBuilder()
                .WithTitle("Server configuration")
                .WithFields(new[]
                {
                    new EmbedFieldBuilder()
                        .WithName(SetupServerCommand.Param_IncomeTaxPercentage)
                        .WithValue(model.IncomeTaxPercentage + "%").WithIsInline(true),
                })
                .Build();

            return rawCommand.RespondAsync(embed: embed);
        }
    }
}
