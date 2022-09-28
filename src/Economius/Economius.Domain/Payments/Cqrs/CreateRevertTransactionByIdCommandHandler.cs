using Economius.Cqrs;
using Economius.Infrastructure.Database;

namespace Economius.Domain.Payments.Cqrs
{
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
            var revertedTransaction = new Transaction(found.ServerId, found.ToUserId, found.FromUserId, found.Amount, found.Comment);
            return session.AddAsync(revertedTransaction);
        }
    }
}
