using Building.Application.Features.Queries.Authentications.GetUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Application.Model.Authentications
{
    public class SignInModelView
    {
        public UserListModel UserListModel { get; set; }
        public string JwtToken { get; set; }
    }
}
