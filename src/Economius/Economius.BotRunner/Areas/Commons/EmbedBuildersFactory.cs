using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.BotRunner.Areas.Commons
{
    public interface IEmbedBuildersFactory
    {
        EmbedBuilder CreateDefaultEmbedBuilder();
    }

    public class EmbedBuildersFactory : IEmbedBuildersFactory
    {
        public EmbedBuilder CreateDefaultEmbedBuilder()
        {
            return new EmbedBuilder()
                .WithAuthor("Economius")
                .WithColor(Color.Purple)
                .WithCurrentTimestamp();
        }
    }
}
