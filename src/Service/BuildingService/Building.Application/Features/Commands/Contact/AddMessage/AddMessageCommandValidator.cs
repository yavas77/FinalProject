using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.Contact.AddMessage
{
    public class AddMessageCommandValidator : AbstractValidator<AddMessageCommand>
    {
        public AddMessageCommandValidator()
        {
            //Content Validation
            RuleFor(x => x.Content)
                .NotEmpty()
                    .WithMessage("Mesaj içeri boş geçilemez!")
                .MinimumLength(10)
                    .WithMessage("Mesaj içeriği en az 10 karakter olmalıdır!")
                .MaximumLength(900)
                    .WithMessage("Mesaj içeriği en fazla 900 karakter olmalıdır!");


        }
    }
}
