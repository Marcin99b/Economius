using Economius.Cqrs;

namespace Economius.Domain.Payments.Cqrs
{
    public class GetWalletQueryResult : IQueryResult
    {
        public Wallet? Wallet { get; }

        public GetWalletQueryResult(Wallet? wallet)
        {
            this.Wallet = wallet;
        }
    }
}
