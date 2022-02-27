using Building.Domain.Entities.Authentications;
using Building.Domain.Entities.Commons;
using Building.Domain.Entities.IncomeAndExpenditure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Domain.Entities.Building
{
    public class Apartment : BaseEntity
    {
        public string ApartmentType { get; set; }
        public int No { get; set; }
        public bool Status { get; set; }
        public int BlockId { get; set; }


        #region NavigationProperties
        public Block Block { get; set; }
        //public ICollection<User> Users { get; set; }
        //public ICollection<ApartmentExpense> UserExpenses { get; set; }
        #endregion

        #region Constructor
        public Apartment()
        {
            //Users = new HashSet<User>();
            //UserExpenses = new HashSet<ApartmentExpense>();
        }
        #endregion
    }
}
