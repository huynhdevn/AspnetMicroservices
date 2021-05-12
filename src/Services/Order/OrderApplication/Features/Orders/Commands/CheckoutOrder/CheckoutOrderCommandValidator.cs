using FluentValidation;

namespace OrderApplication.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty();
            RuleFor(p => p.TotalPrice)
                .GreaterThan(0);
        }
    }
}