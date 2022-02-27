using MediatR;
using System.Collections.Generic;

namespace Building.Application.Features.Queries.Authentications.GetUsers
{
    public class UserListModel 
    { 
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TC { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Plate { get; set; }
        public string Apartment { get; set; }
        public int  ApartmentId { get; set; }
        public string Amount { get; set; }
        public decimal Balance { get; set; }
        public string Owner { get; set; }
        public bool IsActive { get; set; }
        public ICollection<string> Roles { get; set; }

    }
}
