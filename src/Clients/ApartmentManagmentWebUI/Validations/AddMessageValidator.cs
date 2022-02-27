using ApartmentManagmentWebUI.Models.Messages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Validations
{
    public class AddMessageValidator : AbstractValidator<AddMessageModel>
    {
        public AddMessageValidator()
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
