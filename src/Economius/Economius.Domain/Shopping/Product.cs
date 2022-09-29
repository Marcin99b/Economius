using Economius.Infrastructure.Database.Abstraction;

namespace Economius.Domain.Shopping
{
    public class Product : Entity
    {
        public string Name { get; private set; }
        public long Price { get; private set; }

        public Product(string name, long price)
        {
            this.Name = name;
            this.Price = price;
        }
    }
}
