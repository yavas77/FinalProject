using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Models.CaseModels
{
    public enum PaymentType
    {
        [Display(Name = "Gelir")]
        Gelir,
        [Display(Name = "Gider")]
        Gider
    }

    public class DeptPaymentModel
    {
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        [Display(Name = "Ödenecek Tutar")]
        public decimal Amount { get; set; }
        [Display(Name = "Kart Üstündeki İsim")]
        public string FullName { get; set; }
        [Display(Name = "Kart Numarası")]
        public string CardNumber { get; set; }
        [Display(Name = "Yıl")]
        public int Year { get; set; }
        [Display(Name = "Ay")]
        public int Month { get; set; }
        [Display(Name = "Güvenlik Kodu")]
        public int SecurityNumber { get; set; }
        public PaymentType Type { get; set; }
    }
}
