﻿using Discord;
using Economius.BotRunner.Areas.Configuration.Commands;

namespace Economius.BotRunner
{
    public class CommandsConfig
    {
        public SlashCommandProperties[] CommandsInfos { get; } = new[]
        {
            SetupServerCommand.CreateCommandInfo()
        };
    }
}
