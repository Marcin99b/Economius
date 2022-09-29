using Economius.Infrastructure.Database.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Economius.Domain.Shopping
{
    public class Shop : Entity
    {
        private List<Product> products = new List<Product>();

        public ulong ServerId { get; private set; }
        public ulong UserId { get; private set; }
        public ulong WalletId { get; private set; }
        public IEnumerable<Product> Products
        {
            get => this.products;
            protected set => this.products = new List<Product>(value);
        }

        public Shop(ulong serverId, ulong userId, ulong walletId)
        {
            this.ServerId = serverId;
            this.UserId = userId;
            this.WalletId = walletId;
        }

        public void AddProduct(Product product)
        {
            if(this.products.Any(x => x.Name == product.Name))
            {
                throw new ArgumentException($"Product already exist in shop");
            }
            this.products.Add(product);
            this.Update();
        }

        public void UpdateProduct(string name, long newPrice)
        {
            var product = this.products.FirstOrDefault(x => x.Name == name);
            if (product == null)
            {
                throw new ArgumentException($"Product does not exist in shop");
            }
            product.SetPrice(this.Update, newPrice);
        }

        public void RemoveProduct(string name)
        {
            var product = this.products.FirstOrDefault(x => x.Name == name);
            if (product == null)
            {
                throw new ArgumentException($"Product does not exist in shop");
            }
            this.products.Remove(product);
            this.Update();
        }
    }
}
