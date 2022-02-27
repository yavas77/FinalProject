using ApartmentManagmentWebUI.Models.BlockModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Validations
{
    public class AddBlockValidator : AbstractValidator<AddBlockModel>
    {
        public AddBlockValidator()
        {
            //Name Validation
            RuleFor(p => p.Name)
                .NotEmpty()
                    .WithMessage("{Blok Adı} boş geçilemez!")
                .MinimumLength(2)
                    .WithMessage("{Blok Adı} en az 2 karakter olmalıdır!")
                .MaximumLength(10)
                    .WithMessage("{Blok Adı} en fazla 10 karakter olmalıdır!");


            //Floor Validation
            RuleFor(p => p.Floor)
                .GreaterThan(0)
                    .WithMessage("{Kat} bilgisi girilmelidir!");
        }
    }
}
