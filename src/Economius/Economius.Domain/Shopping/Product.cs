using Economius.Infrastructure.Database.Abstraction;

namespace Economius.Domain.Shopping
{
    public class Product
    {
        public string Name { get; private set; }
        public long Price { get; private set; }

        public Product(string name, long price)
        {
            this.Name = name;
            this.Price = price;
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
                Product p => p.Name == this.Name && p.Price == this.Price,
                _ => false
            };
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
