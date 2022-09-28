using Economius.Cqrs;
using Economius.Domain.Payments.Services;
using Economius.Infrastructure.Database;

namespace Economius.Domain.Payments.Cqrs
{
    public class RecalculateWalletBalanceCommandHandler : ICommandHandler<RecalculateWalletBalanceCommand>
    {
        private readonly ISessionFactory sessionFactory;
        private readonly ITransactionsSumCalculator transactionsSumCalculator;

        public RecalculateWalletBalanceCommandHandler(ISessionFactory sessionFactory, ITransactionsSumCalculator transactionsSumCalculator)
        {
            this.sessionFactory = sessionFactory;
            this.transactionsSumCalculator = transactionsSumCalculator;
        }

        //todo improve performance
        public Task HandleAsync(RecalculateWalletBalanceCommand command)
        {
            using var session = this.sessionFactory.CreateMongo();

            var sentTransactions = session.Get<Transaction>().Where(x => x.ServerId == command.ServerId && x.FromUserId == command.UserId);
            var receivedTransactions = session.Get<Transaction>().Where(x => x.ServerId == command.ServerId && x.ToUserId == command.UserId);
            var difference = this.transactionsSumCalculator.Difference(sentTransactions, receivedTransactions);

            var wallet = session.Get<Wallet>().First(x => x.ServerId == command.ServerId && x.UserId == command.UserId);
            wallet.SetBalance(difference);
            return session.UpdateAsync(wallet);
        }
    }
}
