using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Models.Messages
{
    public class AddMessageModel
    {
        [Display(Name ="Mesaj İçerik")]
        public string Content { get; set; }

        [Display(Name = "Konu")]
        public string Title { get; set; }
        public int UserId { get; set; }

    }
}
