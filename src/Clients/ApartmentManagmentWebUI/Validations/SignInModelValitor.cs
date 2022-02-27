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
    public class SignInModelValitor : AbstractValidator<SignInModal>
    {
        public SignInModelValitor()
        {

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


            //Floor Validation
            RuleFor(p => p.Password)
                     .NotEmpty()
                    .WithMessage("Şifre giriniz!");
        }
    }
}
