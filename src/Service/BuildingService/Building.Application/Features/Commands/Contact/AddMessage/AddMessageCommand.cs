using Building.Application.Model.Common;
using Building.Domain.Entities.Contact;
using MediatR;

namespace Building.Application.Features.Commands.Contact.AddMessage
{
    public class AddMessageCommand:IRequest<EntityResult>
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public MessageStatus Status { get; set; }
        public int UserId { get; set; }
    }
}
