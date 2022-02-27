using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.Contact
{
    public class GetAllMessageQuery : IRequest<List<MessageListModel>>
    {
        public GetAllMessageQuery()
        {
        }
    }
}