using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Ordering.Domain.Models;

namespace Ordering.Application.Features.Orders.Commands.Validators
{
    public class UpdateOrderCommandValidation : AbstractValidator<Order>
    {
        public UpdateOrderCommandValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("Id should not empty or null")
                .GreaterThan(0)
                .WithMessage("Id must be grater than 0");

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
