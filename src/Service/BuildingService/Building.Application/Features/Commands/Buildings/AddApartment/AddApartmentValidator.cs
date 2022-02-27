using FluentValidation;

namespace Building.Application.Features.Commands.Buildings.AddApartment
{
    public class AddApartmentValidator : AbstractValidator<AddApartmentCommand>
    {
        public AddApartmentValidator()
        {


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
