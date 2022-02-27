using Building.Domain.Entities.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.Contact
{
    public class MessageListModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public MessageStatus Status { get; set; }
        public string User { get; set; }

 
    }
}
