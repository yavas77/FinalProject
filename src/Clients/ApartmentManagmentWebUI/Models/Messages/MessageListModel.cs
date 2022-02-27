using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagmentWebUI.Models.Messages
{
    #region Enums
    public enum MessageStatus
    {
        [Display(Name = "Yeni")]
        Yeni,
        [Display(Name = "Okunmuş")]
        Okunmus,
        [Display(Name = "Okunmamış")]
        Okunmamis
    }
    #endregion
    public class MessageListModel
    {
        public int Id { get; set; }

        [Display(Name = "İçerik")]
        public string Content { get; set; }
        [Display(Name = "Konu")]
        public string Title { get; set; }
        public MessageStatus Status { get; set; }
        [Display(Name = "Gönderen")]
        public string User { get; set; }
    }
}
