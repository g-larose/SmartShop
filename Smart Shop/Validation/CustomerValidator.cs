using FluentValidation;
using Smart_Shop.Models;
using Smart_Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Shop.Validation
{
    public class CustomerValidator : AbstractValidator<AddCustomerViewModel>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.CompanyName)
                .NotEmpty()
                .WithMessage("Please enter a Company Name");

            RuleFor(c => c.ContactName)
                .NotNull()
                .WithMessage("Please specify a Contact name.");

            RuleFor(c => c.Email)
                .NotNull()
                .WithMessage("Please enter a valid email address.")
                .EmailAddress();

            RuleFor(c => c.Phone)
                .NotNull()
                .WithMessage("Please enter a phone number.");

            RuleFor(c => c.Address)
                .NotNull()
                .WithMessage("Please enter the company address.");
        }
    }
}
