using Economius.Cqrs;
using Economius.Infrastructure.Database;

namespace Economius.Domain.Payments.Cqrs
{
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
            var transaction = new Transaction(command.ServerId, command.FromUserId, command.ToUserId, command.Amount, command.Comment);
            return session.AddAsync(transaction);
        }
    }
}
