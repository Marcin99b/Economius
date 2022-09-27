using Discord;
using Discord.WebSocket;
using Economius.BotRunner.Areas.Commons;
using Economius.BotRunner.Areas.Configuration.Commands;
using Economius.Domain.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.BotRunner.Areas.Configuration.Views
{
    public interface IConfigurationsViews
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
                        .WithName(SetupServerCommand.Param_UserStartMoney) //todo translation
                        .WithValue(model.UserStartMoney).WithIsInline(true),
                    new EmbedFieldBuilder()
                        .WithName(SetupServerCommand.Param_ServerStartMoney)
                        .WithValue(model.ServerStartMoney).WithIsInline(true),
                    new EmbedFieldBuilder()
                        .WithName(SetupServerCommand.Param_IncomeTaxPercentage)
                        .WithValue(model.IncomeTaxPercentage + "%").WithIsInline(true),
                })
                .Build();

            return rawCommand.RespondAsync(embed: embed);
        }
    }
}
