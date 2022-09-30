using Economius.Cqrs;

namespace Economius.Domain.Shopping.Cqrs
{
    public class RemoveProductFromShopCommand : ICommand
    {
        public Guid WalletId { get; }
        public ulong ServerId { get; }
        public ulong UserId { get; }
        public string Identifier { get; }

        private RemoveProductFromShopCommand(string identifier)
        {
            this.Identifier = identifier;
        }

        public RemoveProductFromShopCommand(Guid walletId, string identifier)
            : this(identifier)
        {
            this.WalletId = walletId;
        }

        public RemoveProductFromShopCommand(ulong serverId, ulong userId, string identifier)
            : this(identifier)
        {
            this.ServerId = serverId;
            this.UserId = userId;
        }
    }
}
