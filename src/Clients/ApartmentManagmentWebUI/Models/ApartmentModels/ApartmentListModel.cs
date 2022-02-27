using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApartmentManagmentWebUI.Models.ApartmentModels
{
    public class ApartmentListModel
    {
        public int Id { get; set; }
        public string ApartmentType { get; set; }
        public int No { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }
        public int BlockId { get; set; }
        public string Block { get; set; }
        public SelectList ApartmentList { get; set; }
    }
}
