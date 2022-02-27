using System.Collections.Generic;

namespace ApartmentManagmentWebUI.Models
{
    public class EntityResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
