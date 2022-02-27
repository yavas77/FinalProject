using System.ComponentModel.DataAnnotations;

namespace ApartmentManagmentWebUI.Models.BlockModels
{
    public class UpdateBlockModel
    {
        public int Id { get; set; }
        [Display(Name = "Blok Adı")]
        public string Name { get; set; }

        [Display(Name = "Kat Bilgisi")]
        public int Floor { get; set; }

        [Display(Name = "Aktif/Pasif")]
        public bool IsActive { get; set; }
    }
}
