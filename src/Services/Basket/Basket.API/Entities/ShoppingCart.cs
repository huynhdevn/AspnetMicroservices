using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart() { }

        public ShoppingCart(string username)
        {
            Username = username;
        }

        public string Username { get; set; }
        public IEnumerable<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public decimal TotalPrice => Items.Sum(i => i.Quantity * i.Price);
    }
}
