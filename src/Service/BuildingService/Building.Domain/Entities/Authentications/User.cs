using Building.Domain.Entities.Building;
using Building.Domain.Entities.Contact;
using Building.Domain.Entities.IncomeAndExpenditure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Domain.Entities.Authentications
{
    public enum OwnerType
    {
        Evsahibi,
        Kiracı
    }

    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TC { get; set; }
        public string Owner { get; set; }
        public string Plate { get; set; }
        public decimal Balance { get; set; }
        public OwnerType Type { get; set; }
        public int? ApartmentId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }


        #region NavigationProperties
        public Apartment Apartment { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<ApartmentExpense> ApartmentExpenses { get; set; }
        #endregion

        #region Constructor

        public User()
        {
            Messages = new HashSet<Message>();
            ApartmentExpenses = new HashSet<ApartmentExpense>();
        }

        #endregion

    }
}
