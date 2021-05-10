﻿namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class OrderVm
    {
        public int Id { get; protected set; }
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        
        // Billing Address
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        // Payment
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public int PaymentMethod { get; set; }
    }
}