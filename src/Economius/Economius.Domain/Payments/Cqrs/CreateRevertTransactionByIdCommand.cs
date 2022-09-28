using Economius.Cqrs;

namespace Economius.Domain.Payments.Cqrs
{
    public class CreateRevertTransactionByIdCommand : ICommand
    {
        public Guid TransactionId { get; }

        public CreateRevertTransactionByIdCommand(Guid transactionId)
        {
            this.TransactionId = transactionId;
        }
    }
}
