using Economius.Cqrs;
using Economius.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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

    public class CreateRevertTransactionByIdCommand : ICommand
    {
        public Guid TransactionId { get; }

        public CreateRevertTransactionByIdCommand(Guid transactionId)
        {
            this.TransactionId = transactionId;
        }
    }

    public class CreateRevertTransactionByIdCommandHandler : ICommandHandler<CreateRevertTransactionByIdCommand>
    {
        private readonly ISessionFactory sessionFactory;

        public CreateRevertTransactionByIdCommandHandler(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public Task HandleAsync(CreateRevertTransactionByIdCommand command)
        {
            using var session = this.sessionFactory.CreateMongo();
            var found = session.Get<Transaction>(command.TransactionId);
            if(found == null)
            {
                throw new ArgumentException($"Transaction with id {command.TransactionId} does not exist");
            }
            var revertedTransaction = new Transaction(found.ServerId, found.ToUserId, found.FromUserId, found.Amount);
            return session.AddAsync(revertedTransaction);
        }
    }

    
}
