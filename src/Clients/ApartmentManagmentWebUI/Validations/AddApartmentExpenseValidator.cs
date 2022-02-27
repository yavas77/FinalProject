using ApartmentManagmentWebUI.Models.IncomeAndExpenditure;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Validations
{
    public class AddApartmentExpenseValidator : AbstractValidator<AddApartmentExpenseModel>
    {
        public AddApartmentExpenseValidator()
        {
            //UserId Validation
            RuleFor(x => x.UserId)
                .GreaterThan(-1)
            .WithMessage("Kullanıcı {Id} si girilmelidir!");

            //Bill Validation
            RuleFor(x => x.Bill)
                .NotEmpty()
                    .WithMessage("{Fatura Bilgisi} boş geçilemez!")
                .MinimumLength(2)
                    .WithMessage("{Fatura Bilgisi} 5 en az karakter olmalıdır!")
                .MaximumLength(180)
                    .WithMessage("{Fatura Bilgisi} 180 en fazla karakter olmalıdır!");

            //Amount Validation
            RuleFor(p => p.Amount)
                .NotEmpty().WithMessage("Fatura tutar bilgisi girilmelidir")
                .GreaterThan(0)
                    .WithMessage("{Fatura Tutarı} girilmelidir!");

            //Mounth Validation
            RuleFor(x => x.Month)
                .NotEmpty().WithMessage("Fatura ay bilgisi girilmelidir")
                .GreaterThan(0)
                .WithMessage("Fatura ay bilgisi girilmelidir!")
                .LessThan(13)
                .WithMessage("Geçerli ay bilgisi giriniz!");

            //Year Validation
            RuleFor(x => x.Year)
                .NotEmpty().WithMessage("Fatura yıl bilgisi girilmelidir")
                .GreaterThan(0)
                .WithMessage("Fatura yıl bilgisi girilmelidir!")
                .LessThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("{Yıl} bulunduğumuz yıldan büyük olamaz!");




        }
    }
}
