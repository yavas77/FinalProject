using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Models.CaseModels
{
    public class CaseListModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
