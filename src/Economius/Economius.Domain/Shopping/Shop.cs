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
        /// <summary>
        /// Wallet ID is single shop identifier, there is relation one to one.
        /// Server and User ID should be used only for optimization/user experience reasons.
        /// </summary>
        public Guid WalletId { get; private set; }
        public ulong ServerId { get; private set; }
        public ulong UserId { get; private set; }
        public IEnumerable<Product> Products
        {
            get => this.products;
            protected set => this.products = new List<Product>(value);
        }

        public Shop(Guid walletId, ulong serverId, ulong userId)
        {
            this.WalletId = walletId;
            this.ServerId = serverId;
            this.UserId = userId;
        }

        public void AddProduct(Product product)
        {
            if(this.products.Any(x => x.Identifier.ToLower() == product.Identifier.ToLower()))
            {
                throw new ArgumentException($"Product already exist in shop");
            }
            this.products.Add(product);
            this.Update();
        }

        public void UpdateProduct(Product newProduct)
        {
            var product = this.products.FirstOrDefault(x => x.Identifier.ToLower() == newProduct.Identifier.ToLower());
            if (product == null)
            {
                throw new ArgumentException($"Product does not exist in shop");
            }
            product.SetDescription(this.Update, newProduct.Description);
            product.SetPrice(this.Update, newProduct.Price);
        }

        public void RemoveProduct(string identifier)
        {
            var product = this.products.FirstOrDefault(x => x.Identifier.ToLower() == identifier.ToLower());
            if (product == null)
            {
                throw new ArgumentException($"Product does not exist in shop");
            }
            this.products.Remove(product);
            this.Update();
        }
    }
}
