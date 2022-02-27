using MediatR;
using System.Collections.Generic;

namespace Building.Application.Features.Queries.Buildings.GetBlocks
{
    public class GetBlockListQuery : IRequest<List<BlockListModel>>
    {
        public GetBlockListQuery()
        {
        }
    }
}