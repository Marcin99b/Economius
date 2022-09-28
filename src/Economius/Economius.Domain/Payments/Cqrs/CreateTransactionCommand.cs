using Economius.Cqrs;
using Economius.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.Domain.Payments.Cqrs
{
    public class CreateTransactionCommand : ICommand
    {
        public ulong ServerId { get; }
        public ulong FromUserId { get; }
        public ulong ToUserId { get; }
        public long Amount { get; }

        public CreateTransactionCommand(ulong serverId, ulong fromUserId, ulong toUserId, long amount)
        {
            this.ServerId = serverId;
            this.FromUserId = fromUserId;
            this.ToUserId = toUserId;
            this.Amount = amount;
        }
    }

    public class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand>
    {
        private readonly ISessionFactory sessionFactory;

        public CreateTransactionCommandHandler(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public Task HandleAsync(CreateTransactionCommand command)
        {
            using var session = this.sessionFactory.CreateMongo();
            var transaction = new Transaction(command.ServerId, command.FromUserId, command.ToUserId, command.Amount);
            return session.AddAsync(transaction);
        }
    }

    
}
