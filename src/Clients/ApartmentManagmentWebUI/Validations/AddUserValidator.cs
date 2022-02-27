using ApartmentManagmentWebUI.Models.UserModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Validations
{
    public class AddUserValidator : AbstractValidator<UserAddModel>
    {
        public AddUserValidator()
        {

            //FirstName Validation
            RuleFor(x => x.FirstName)
                .NotEmpty()
                    .WithMessage("{Ad} boş geçilemez!")
                .MaximumLength(30)
                    .WithMessage("{Ad} en fazla 30 karakter olabilir!")
                .MinimumLength(2)
                    .WithMessage("{Ad} en az 2 karakter olmalıdır!");

            //Owner Validation
            RuleFor(x => x.Owner)
                .NotEmpty()
                    .WithMessage("{Ad} boş geçilemez!");

            //Email Validation
            RuleFor(x => x.Email)
                .NotEmpty()
                    .WithMessage("{Email Adres} boş geçilemez!")
                .MaximumLength(150)
                    .WithMessage("Email en fazla 150 karakter olabilir!")
                .Must(p => p != null && Regex.IsMatch(p, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                    .WithMessage("Eposta uygun biçimde girilmemiş!");

            //Phone Validation
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                    .WithMessage("{Telefon} boş geçilemez!")
                .Length(10)
                    .WithMessage("{Telefon} 10 karakter olmalıdır!");

            //TC Validation
            RuleFor(x => x.TC)
                .NotEmpty()
                    .WithMessage("{TC} boş geçilemez!")
                .Length(11)
                    .WithMessage("{TC} 11 karakter olmalıdır!");

        }
    }
}
