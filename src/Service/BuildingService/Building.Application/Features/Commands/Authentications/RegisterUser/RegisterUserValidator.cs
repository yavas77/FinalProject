using FluentValidation;
using System.Text.RegularExpressions;

namespace Building.Application.Features.Commands.Authentications.RegisterUser
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {

            //FirstName Validation
            RuleFor(x => x.FirstName)
                .NotEmpty()
                    .WithMessage("{Ad} boş geçilemez!")
                .MaximumLength(30)
                    .WithMessage("{Ad} en fazla 30 karakter olabilir!")
                .MinimumLength(2)
                    .WithMessage("{Ad} en az 2 karakter olmalıdır!");

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
