using FluentValidation;

namespace Building.Application.Features.Commands.Buildings.UpdateApartment
{
    public class UpdateApartmentValidator : AbstractValidator<UpdateApartmentCommand>
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
                .MaximumLength(3)
                    .WithMessage("{Apartman Tipi} 3 karakter olmalıdır!")
                .MinimumLength(3)
                    .WithMessage("{Apartman Tipi} 3 karakter olmalıdır!");


            //No Validation
            RuleFor(p => p.No)
                .NotEmpty()
                    .WithMessage("{Apartman Tipi} boş geçilemez!");
        }
    }
}
