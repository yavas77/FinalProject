using Building.Domain.Entities.Authentications;
using Building.Domain.Entities.Building;
using Building.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Domain.Entities.IncomeAndExpenditure
{
    public enum PaymentStatus
    {

        Beklemede,
        Ödendi
    }

    public class ApartmentExpense : BaseEntity
    {
        #region Properties

        public string Bill { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public int UserId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        #endregion

        #region Constructors

        public ApartmentExpense()
        {
            Status = PaymentStatus.Beklemede;
        }

        #endregion

        #region NavigationProperties
        public User User { get; set; }
        #endregion
    }


}
