using Discord;
using Discord.WebSocket;
using Economius.BotRunner.Areas.Configuration.Commands;
using Economius.BotRunner.Areas.Configuration.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

namespace Economius.BotRunner.Areas.Commons
{
    public class EmptyViewModel : IViewModel
    {
        public string? Message { get; }

        public EmptyViewModel(string? message = null)
        {
            this.Message = message;
        }
    }

    public class SuccessViewModel : IViewModel
    {
        public string? Message { get; }

        public SuccessViewModel(string? message = null)
        {
            this.Message = message;
        }
    }

    public class ErrorViewModel : IViewModel
    {
        public string? Message { get; }

        public ErrorViewModel(string? message = null)
        {
            this.Message = message;
        }
    }

    public interface ICommonViews : IViewsService
    {
        Task EmptyView(SocketSlashCommand rawCommand, EmptyViewModel model);
        Task Success(SocketSlashCommand rawCommand, SuccessViewModel model);
        Task Error(SocketSlashCommand rawCommand, ErrorViewModel model);
    }

    public class CommonViews : ICommonViews
    {
        private readonly IEmbedBuildersFactory embedBuildersFactory;

        public CommonViews(IEmbedBuildersFactory embedBuildersFactory)
        {
            this.embedBuildersFactory = embedBuildersFactory;
        }

        public Task EmptyView(SocketSlashCommand rawCommand, EmptyViewModel model)
        {
            if(string.IsNullOrWhiteSpace(model.Message))
            {
                return this.Success(rawCommand, new SuccessViewModel());
            }
            var embed = this.embedBuildersFactory
                .CreateDefaultEmbedBuilder()
                .WithTitle("Message")
                .WithDescription(model.Message)
                .Build();

            return rawCommand.RespondAsync(embed: embed);
        }

        public Task Success(SocketSlashCommand rawCommand, SuccessViewModel model)
        {
            var embedBuilder = this.embedBuildersFactory
                .CreateDefaultEmbedBuilder()
                .WithTitle("Success")
                .WithColor(Color.Green);
            if(!string.IsNullOrWhiteSpace(model.Message))
            {
                embedBuilder.WithDescription(model.Message);
            }
            return rawCommand.RespondAsync(embed: embedBuilder.Build());
        }

        public Task Error(SocketSlashCommand rawCommand, ErrorViewModel model)
        {
            var embedBuilder = this.embedBuildersFactory
                .CreateDefaultEmbedBuilder()
                .WithTitle("Error")
                .WithColor(Color.Red);
            if (!string.IsNullOrWhiteSpace(model.Message))
            {
                embedBuilder.WithDescription(model.Message);
            }
            return rawCommand.RespondAsync(embed: embedBuilder.Build());
        }
    }
}
