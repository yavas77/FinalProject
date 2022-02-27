using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.Authentications.GetUsers
{
    public class GetUserByEmailAndPasswordQuery : IRequest<UserListModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public GetUserByEmailAndPasswordQuery(string email, string password)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Password = password ?? throw new ArgumentNullException(nameof(password));

        }
    }
}
