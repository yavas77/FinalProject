using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Models.IncomeAndExpenditure
{
    public class UpdateApartmentExpenseModel
    {
        public int Id { get; set; }

        [Display(Name = "Fatura")]
        public string Bill { get; set; }

        [Display(Name = "Tutar")]
        public decimal Amount { get; set; }

        [Display(Name = "Kullanıcı")]
        public int UserId { get; set; }

        [Display(Name = "Dönemi")]
        public int Month { get; set; }

        [Display(Name = "Yılı")]
        public int Year { get; set; }
    }
}
