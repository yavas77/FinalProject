using Building.Domain.Entities.Authentications;
using Building.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Domain.Entities.IncomeAndExpenditure
{
    public enum PaymentType
    {
        [Display(Name ="Gelir")]
        Gelir,
        [Display(Name ="Gider")]
        Gider
    }

    public class Case:BaseEntity
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public PaymentType Type { get; set; }


		#region NavigationProperties

		public User User { get; set; }

		#endregion
	}
}
