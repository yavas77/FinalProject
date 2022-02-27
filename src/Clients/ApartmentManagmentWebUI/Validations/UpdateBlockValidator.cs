using ApartmentManagmentWebUI.Models.BlockModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Validations
{
    public class UpdateBlockValidator : AbstractValidator<UpdateBlockModel>
    {
        public UpdateBlockValidator()
        {

            //Id Validation
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id girilmelidir!");

            //Name Validation
            RuleFor(p => p.Name)
                .NotEmpty()
                    .WithMessage("{Blok Adı} boş geçilemez!")
                .MinimumLength(2)
                    .WithMessage("{Blok Adı} 2 en az karakter olmalıdır!")
                .MaximumLength(10)
                    .WithMessage("{Blok Adı} 10 en fazla karakter olmalıdır!");


            //Floor Validation
            RuleFor(p => p.Floor)
                .GreaterThan(0)
                    .WithMessage("{Kat} bilgisi girilmelidir!");
        }
    }
}
