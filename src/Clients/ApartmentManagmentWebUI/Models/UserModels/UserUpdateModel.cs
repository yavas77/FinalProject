namespace ApartmentManagmentWebUI.Models.UserModels
{
    public class UserUpdateModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Owner { get; set; }
        public string TC { get; set; }
        public string Plate { get; set; }
        public int ApartmentId { get; set; }
    }
}
