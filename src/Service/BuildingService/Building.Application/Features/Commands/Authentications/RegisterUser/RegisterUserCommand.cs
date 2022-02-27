using Building.Application.Model.Common;
using MediatR;

namespace Building.Application.Features.Commands.Authentications.RegisterUser
{
    public class RegisterUserCommand : IRequest<EntityResult>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string TC { get; set; }
        public string Owner { get; set; }
        public string Plate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public RegisterUserCommand()
        {
            IsActive = true;
            IsDelete = true;
        }
    }
}
