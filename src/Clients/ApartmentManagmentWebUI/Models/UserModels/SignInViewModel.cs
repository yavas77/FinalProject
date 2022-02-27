using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Models.UserModels
{
    public class SignInViewModel
    {
        public UserListModel UserListModel { get; set; }
        public string JwtToken { get; set; }
    }
}
