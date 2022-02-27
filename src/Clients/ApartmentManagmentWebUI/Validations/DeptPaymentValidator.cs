using ApartmentManagmentWebUI.Models.CaseModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Validations
{
    public class DeptPaymentValidator : AbstractValidator<DeptPaymentModel>
    {
        public DeptPaymentValidator()
        {
            //FullName Validation
            RuleFor(x => x.FullName)
                .NotEmpty()
                    .WithMessage("Kart üstündeki ismi giriniz!");


            //CardNumber Validation
            RuleFor(p => p.CardNumber)
               .NotEmpty()
                    .WithMessage("Kart numarasını giriniz giriniz!")
               .Length(16)
                    .WithMessage("Kart numarası 16 karakter olmalıdır!");

            //Year Validation
            RuleFor(p => p.Year)
               .NotEmpty()
                    .WithMessage("Kartın kullanım yıl bilgisini giriniz!");

            //Month Validation
            RuleFor(p => p.Month)
               .NotEmpty()
                    .WithMessage("Kartın kullanım ay bilgisini giriniz!");

            //SecurityNumber Validation
            RuleFor(p => p.SecurityNumber)
               .NotEmpty()
                    .WithMessage("Kartın güvenlik kodu bilgisini giriniz!");

            //Amount Validation
            RuleFor(p => p.Amount)
               .NotEmpty()
                    .WithMessage("Ödenecek tutarı giriniz!")
               .GreaterThan(0)
                    .WithMessage("Ödenecek tutarı giriniz!");

        }
    }
}
