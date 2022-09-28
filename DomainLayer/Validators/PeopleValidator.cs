using DomainLayer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Validators
{
    public class PeopleValidator: AbstractValidator<People>
    {
        public PeopleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Matches(@"^[a-zA-Z ]*$")
                .WithMessage("Name cannot be left empty and must not contain any alphanumeric characters");
            
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email must be valid");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^[0-9]{10}$")
                .WithMessage("Phone number must be 10 characters long with only numeric characters");
            RuleFor(x => x.PaymentType).NotEmpty();
        }
    }
}
