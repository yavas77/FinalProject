﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Models.UserModels
{
    public class LoginSessionModel
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string JwtToken { get; set; }
    }
}
