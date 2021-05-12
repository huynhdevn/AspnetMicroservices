using System.Collections.Generic;
using BasketApi.Entities;

namespace BasketApi.Models
{
    public class BasketCheckout
    {
        public string UserName { get; set; }
        
        // Billing Address
        public string FullName { get; set; }
        public string Address { get; set; }

        // Payment
        public int PaymentMethod { get; set; }

    }
}