using System.Collections.Generic;

namespace BasketApi.Entities
{
    public class Basket
    {
        public Basket(string userName)
        {
            UserName = userName;
        }
        
        public string UserName { get; set; }
        public IEnumerable<BasketItem> Items { get; set; }
    }
}