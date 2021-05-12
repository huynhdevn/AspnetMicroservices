namespace EventBus.Messages.Events
{
    public class BasketCheckoutEvent //: IntegrationBaseEvent
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