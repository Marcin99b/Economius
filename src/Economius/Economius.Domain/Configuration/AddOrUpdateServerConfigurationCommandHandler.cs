using Economius.Cqrs;
using Economius.Infrastructure.Database;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Economius.Domain.Configuration
{
    public class AddOrUpdateServerConfigurationCommandHandler : ICommandHandler<AddOrUpdateServerConfigurationCommand>
    {
        private readonly ISessionFactory sessionFactory;

        public AddOrUpdateServerConfigurationCommandHandler(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public Task HandleAsync(AddOrUpdateServerConfigurationCommand command)
        {
            using var session = this.sessionFactory.CreateMongo();

            var found = session.Get<ServerConfiguration>().FirstOrDefault(x => x.ServerId == command.ServerId);
            if(found != null)
            {
                found.SetUserStartMoney(command.UserStartMoney);
                found.SetServerStartMoney(command.ServerStartMoney);
                found.SetIncomeTaxPercentage(command.IncomeTaxPercentage);
                return session.UpdateAsync(found);
            }
            var serverConfiguration = new ServerConfiguration(command.ServerId, command.UserStartMoney, command.ServerStartMoney, command.IncomeTaxPercentage);
            return session.AddAsync(serverConfiguration);
        }
    }
}
