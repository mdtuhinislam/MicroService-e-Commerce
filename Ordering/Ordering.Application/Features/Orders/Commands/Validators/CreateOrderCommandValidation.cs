using FluentValidation;
using Ordering.Application.Features.Orders.Commands.CreateOrder;

namespace Ordering.Application.Features.Orders.Commands.Validators
{
    public class CreateOrderCommandValidation : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidation()
        {
            RuleFor(c => c.UserName)
                .NotEmpty()
                .WithMessage("Please enter user name")
                .EmailAddress().WithMessage("User name should be an email address");
            RuleFor(c => c.EmailAddress)
                .EmailAddress().WithMessage("Email should be valid");
            RuleFor(c => c.FirstName)
                .NotEmpty()
                .MinimumLength(3).WithMessage("Enter first name with minimum 3 characters");
            RuleFor(c => c.TotalPrice)
                .GreaterThan(0).WithMessage("Total price should be grater than 0");
        }
    }
}
