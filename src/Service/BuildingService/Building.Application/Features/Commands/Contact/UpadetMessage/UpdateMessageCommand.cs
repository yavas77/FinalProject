using Building.Application.Model.Common;
using Building.Domain.Entities.Contact;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.Contact.UpadetMessage
{
    public class UpdateMessageCommand : IRequest<EntityResult>
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public MessageStatus Status { get; set; }
        public int UserId { get; set; }
    }
}
