using OrderDomain.Common;

namespace OrderDomain.Entities
{
    public class Order : EntityBase, IAggregateRoot
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
    }
}