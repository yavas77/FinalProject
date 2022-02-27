using ApartmentManagmentWebUI.Models.ApartmentModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Validations
{
    public class UpdateApartmentValidator : AbstractValidator<UpdateApartmentModel>
    {
        public UpdateApartmentValidator()
        {
            //Id Validation
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id girilmelidir!");

            //ApartmentType Validation
            RuleFor(x => x.ApartmentType)
                .NotEmpty()
                    .WithMessage("{Apartman Tipi} boş geçilemez!")
                .Length(3)
                    .WithMessage("{Apartman Tipi} 3 karakter olmalıdır!");


            //No Validation
            RuleFor(p => p.No)
                .NotEmpty()
                    .WithMessage("{Apartman Tipi} boş geçilemez!");
        }
    }
}