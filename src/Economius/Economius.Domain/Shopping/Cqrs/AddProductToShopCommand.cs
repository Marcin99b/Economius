using Economius.Cqrs;

namespace Economius.Domain.Shopping.Cqrs
{
    public class AddProductToShopCommand : ICommand
    {
        public Guid WalletId { get; }
        public ulong ServerId { get; }
        public ulong UserId { get; }
        public string Identifier { get; }
        public string Description { get; }
        public long Price { get; }

        private AddProductToShopCommand(string identifier, string description, long price)
        {
            this.Identifier = identifier;
            this.Description = description;
            this.Price = price;
        }

        public AddProductToShopCommand(Guid walletId, string identifier, string description, long price) 
            : this(identifier, description, price)
        {
            this.WalletId = walletId;
        }

        public AddProductToShopCommand(ulong serverId, ulong userId, string identifier, string description, long price) 
            : this(identifier, description, price)
        {
            this.ServerId = serverId;
            this.UserId = userId;
        }
    }
}
