using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderApplication.Contracts;
using OrderApplication.Models;
using OrderDomain.Entities;
using OrderDomain.Repositories;

namespace OrderApplication.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepo,
            IMapper mapper, IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<CheckoutOrderCommand, Order>(request);

            var newOrder = await _orderRepo.AddAsync(order);

            _logger.LogInformation($"Order {newOrder.Id} is successfully created");
            await SendEmail(newOrder);

            return newOrder.Id;
        }

        private async Task SendEmail(Order order)
        {
            var email = new Email()
            {
                To = "huynhdev@hotmail.com",
                Subject = "Order was created",
                Body = "Order was created"
            };
            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {order.Id} failed due to an error with the email service: {ex.Message}");
            }
        }
    }
}