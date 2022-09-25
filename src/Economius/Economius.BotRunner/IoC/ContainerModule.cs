using Autofac;
using Economius.BotRunner.IoC.Modules;
using Economius.Infrastructure.Database.Abstraction;
using Economius.IoC.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.BotRunner.IoC
{
    [ExcludeFromCodeCoverage]
    public class ContainerModule
    {
        private readonly string mongoConnectionString;

        public ContainerModule(string mongoConnectionString)
        {
            this.mongoConnectionString = mongoConnectionString;
        }

        public ContainerBuilder GetBuilder()
        {
            var builder = new ContainerBuilder();
            return this.FillBuilder(builder);
        }

        public ContainerBuilder FillBuilder(ContainerBuilder builder)
        {
            builder.RegisterModule(new DatabaseModule(this.mongoConnectionString));
            builder.RegisterModule<CommandModule>();
            builder.RegisterModule<QueryModule>();
            builder.RegisterModule<ServiceModule>();
            //builder.RegisterModule<ControllerModule>();
            return builder;
        }
    }
}
