using System.ComponentModel.DataAnnotations;

namespace ApartmentManagmentWebUI.Models.ApartmentModels
{
    public class UpdateApartmentModel
    {
        public int Id { get; set; }
        [Display(Name = "Apartman Türü")]
        public string ApartmentType { get; set; }

        [Display(Name = "Daire No")]
        public int No { get; set; }

        [Display(Name = "Bulunduğu Blok")]
        public int BlockId { get; set; }

        [Display(Name = "Aktif/Pasif")]
        public bool Status { get; set; }
    }
}
