using Building.Application.Model.Common;
using MediatR;

namespace Building.Application.Features.Commands.Authentications.UpdateUser
{
    public class UpdateUserCommand : IRequest<EntityResult>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string TC { get; set; }
        public string Plate { get; set; }
        public int ApartmentId { get; set; }
        public string Owner { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public UpdateUserCommand()
        {
            IsActive = true;
            IsDelete = true;
        }


    }
}
