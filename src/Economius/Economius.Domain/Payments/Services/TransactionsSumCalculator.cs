using MongoDB.Driver.Core.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Economius.Domain.Payments.Services
{
    public interface ITransactionsSumCalculator
    {
        long Calculate(IEnumerable<Transaction> transactions);
        long Calculate(ulong serverId, DateTime fromDateTime, ulong fromUserId, ulong toUserId, IEnumerable<Transaction> transactions);
        long Difference(IEnumerable<Transaction> oldTransactions, IEnumerable<Transaction> newTransactions);
    }

    public class TransactionsSumCalculator : ITransactionsSumCalculator
    {
        //todo check performance of ienumerable vs other options
        public long Calculate(ulong serverId, DateTime fromDateTime, ulong fromUserId, ulong toUserId, IEnumerable<Transaction> transactions)
        {
            long sum = 0;
            //todo check performance
            foreach (var transaction in transactions)
            {
                if (transaction.ServerId != serverId)
                {
                    continue;
                }
                if (transaction.FromUserId != fromUserId)
                {
                    continue;
                }
                if (transaction.ToUserId != toUserId)
                {
                    continue;
                }
                if (transaction.CreatedAt < fromDateTime)
                {
                    continue;
                }

                sum += transaction.Amount;
            }
            return sum;
        }

        public long Calculate(IEnumerable<Transaction> transactions)
        {
            long sum = 0;
            //todo check performance
            foreach (var transaction in transactions)
            {
                sum += transaction.Amount;
            }
            return sum;
        }

        public long Difference(IEnumerable<Transaction> oldTransactions, IEnumerable<Transaction> newTransactions)
        {
            var oldSum = this.Calculate(oldTransactions);
            var newSum = this.Calculate(newTransactions);
            return newSum - oldSum;
        }
    }
}
