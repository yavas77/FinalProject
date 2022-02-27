using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApartmentManagmentWebUI.Models.UserModels
{
    public class SetUserApartmentModel
    {
        public int UserId { get; set; }
        public int ApartmentId { get; set; }

        public SelectList ApartmentList { get; set; }
    }
}
