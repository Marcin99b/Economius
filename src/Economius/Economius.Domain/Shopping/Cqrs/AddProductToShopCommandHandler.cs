using Economius.Cqrs;
using Economius.Infrastructure.Database;

namespace Economius.Domain.Shopping.Cqrs
{
    public class AddProductToShopCommandHandler : ICommandHandler<AddProductToShopCommand>
    {
        private readonly ISessionFactory sessionFactory;

        public AddProductToShopCommandHandler(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public Task HandleAsync(AddProductToShopCommand command)
        {
            using var session = this.sessionFactory.CreateMongo();

            var shop = command.WalletId != default
                ? session.Get<Shop>(command.WalletId)!
                : session.Get<Shop>()
                    .FirstOrDefault(x => x.ServerId == command.ServerId && x.UserId == command.UserId)
                    ?? throw new ArgumentException($"Shop for user {command.UserId} does not exist on server {command.ServerId}.");

            var product = new Product(command.Identifier, command.Description, command.Price);
            shop.AddProduct(product);

            return session.UpdateAsync(shop);
        }
    }
}
