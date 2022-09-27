using Economius.Cqrs;
using Economius.Infrastructure.Database;
using MongoDB.Driver;

namespace Economius.Domain.Payments.Cqrs
{
    public class CreateWalletCommandHandler : ICommandHandler<CreateWalletCommand>
    {
        private readonly ISessionFactory sessionFactory;

        public CreateWalletCommandHandler(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public Task HandleAsync(CreateWalletCommand command)
        {
            using var session = this.sessionFactory.CreateMongo();
            var found = session.Get<Wallet>().FirstOrDefault(x => x.ServerId == command.ServerId && x.UserId == command.UserId);
            if(found != null)
            {
                //todo log and try prevent
                return Task.CompletedTask;
            }
            var wallet = new Wallet(command.ServerId, command.UserId);
            return session.AddAsync(wallet);
        }
    }
}
