using Economius.Infrastructure.Database.Abstraction;
using System.Diagnostics.CodeAnalysis;

namespace Economius.Domain.Shopping
{
    public class Product
    {
        public string Identifier { get; private set; }
        public long Price { get; private set; }
        public string Description { get; set; }

        public Product(string identifier, string description, long price)
        {
            this.Identifier = identifier;
            this.Description = description;
            this.Price = price;
        }

        internal void SetDescription(Action update, string description)
        {
            if (this.Description == description)
            {
                return;
            }
            this.Description = description;
            update.Invoke();
        }

        internal void SetPrice(Action update, long price)
        {
            if(this.Price == price)
            {
                return;
            }
            this.Price = price;
            update.Invoke();
        }

        public override bool Equals(object? obj)
        {
            return obj switch
            {
                Product p => p.Identifier == this.Identifier,
                _ => false
            };
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
