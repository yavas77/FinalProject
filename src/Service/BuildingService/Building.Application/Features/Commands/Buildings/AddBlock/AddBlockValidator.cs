using FluentValidation;

namespace Building.Application.Features.Commands.Buildings.AddBlock
{
    public class AddBlockValidator : AbstractValidator<AddBlockCommand>
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
