using System.ComponentModel.DataAnnotations;
using MediatR;

namespace OrderApplication.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommand : IRequest<int>
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

        // Billing Address
        public string FullName { get; set; }
        public string Address { get; set; }

        // Payment
        public int PaymentMethod { get; set; }

    }
}