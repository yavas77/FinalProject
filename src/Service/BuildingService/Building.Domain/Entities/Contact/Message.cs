using Building.Domain.Entities.Authentications;
using Building.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Domain.Entities.Contact
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

    public class Message : BaseEntity
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public MessageStatus Status { get; set; }
        public int UserId { get; set; }

        #region Navigation Properties

        public User User { get; set; }

        #endregion
    }
}