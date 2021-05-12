using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OrderDomain.Entities;
using OrderDomain.Repositories;

namespace OrderApplication.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, IEnumerable<OrderVm>>
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IMapper _mapper;

        public GetOrderListQueryHandler(IOrderRepository orderRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderVm>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepo.GetByUserName(request.UserName);

            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderVm>>(orders);
        }
    }
}