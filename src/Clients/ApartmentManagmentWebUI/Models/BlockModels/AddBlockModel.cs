using System.ComponentModel.DataAnnotations;

namespace ApartmentManagmentWebUI.Models.BlockModels
{
    public class AddBlockModel
    {
        [Display(Name = "Blok Adı")]
        public string Name { get; set; }

        [Display(Name = "Kat Bilgisi")]
        public int Floor { get; set; }
    }
}
