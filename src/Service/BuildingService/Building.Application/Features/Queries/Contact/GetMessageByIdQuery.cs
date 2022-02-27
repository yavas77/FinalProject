using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.Contact
{
    public class GetMessageByIdQuery : IRequest<MessageListModel>
    {
        internal int Id { get; set; }
        public GetMessageByIdQuery(int id)
        {
            Id = id;
        }
    }
}