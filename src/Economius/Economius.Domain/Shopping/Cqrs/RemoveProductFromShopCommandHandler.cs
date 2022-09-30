using Economius.Cqrs;
using Economius.Infrastructure.Database;

namespace Economius.Domain.Shopping.Cqrs
{
    public class RemoveProductFromShopCommandHandler : ICommandHandler<RemoveProductFromShopCommand>
    {
        private readonly ISessionFactory sessionFactory;

        public RemoveProductFromShopCommandHandler(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public Task HandleAsync(RemoveProductFromShopCommand command)
        {
            using var session = this.sessionFactory.CreateMongo();

            var shop = command.WalletId != default
                ? session.Get<Shop>(command.WalletId)!
                : session.Get<Shop>()
                    .FirstOrDefault(x => x.ServerId == command.ServerId && x.UserId == command.UserId)
                    ?? throw new ArgumentException($"Shop for user {command.UserId} does not exist on server {command.ServerId}.");

            shop.RemoveProduct(command.Identifier);

            return session.UpdateAsync(shop);
        }
    }
}
