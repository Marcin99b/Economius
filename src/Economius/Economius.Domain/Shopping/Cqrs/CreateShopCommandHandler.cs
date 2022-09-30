using Economius.Cqrs;
using Economius.Infrastructure.Database;

namespace Economius.Domain.Shopping.Cqrs
{
    public class CreateShopCommandHandler : ICommandHandler<CreateShopCommand>
    {
        private readonly ISessionFactory sessionFactory;

        public CreateShopCommandHandler(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public Task HandleAsync(CreateShopCommand command)
        {
            using var session = this.sessionFactory.CreateMongo();
            var found = session.Get<Shop>().FirstOrDefault(x => x.WalletId == command.WalletId);
            if(found != null)
            {
                throw new ArgumentException($"Shop for wallet ID {command.WalletId} already exist.");
            }
            var shop = new Shop(command.WalletId, command.ServerId, command.UserId);
            //todo delete this code
            //only for test environment
#if DEBUG
            shop.AddProduct(new Product("Test", "Opis produktu A", 21));
            shop.AddProduct(new Product("Test ABC", "Opis produktu B", 37));
#endif
            return session.AddAsync(shop);
        }
    }
}
