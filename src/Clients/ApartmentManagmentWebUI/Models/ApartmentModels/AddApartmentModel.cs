using System.ComponentModel.DataAnnotations;

namespace ApartmentManagmentWebUI.Models.ApartmentModels
{
    public class AddApartmentModel
    {
        [Display(Name = "Apartman Türü")]
        public string ApartmentType { get; set; }

        [Display(Name = "Daire No")]
        public int No { get; set; }
        //public bool State { get; set; }

        [Display(Name = "Bulunduğu Blok")]
        public int BlockId { get; set; }
    }
}
