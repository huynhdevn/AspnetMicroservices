using System.Collections.Generic;
using MediatR;

namespace OrderApplication.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListQuery : IRequest<IEnumerable<OrderVm>>
    {
        public GetOrderListQuery(string userName)
        {
            this.UserName = userName;
        }
        
        public string UserName { get; }
    }
}