using MediatR;

namespace Building.Application.Features.Queries.Buildings.GetBlocks
{
    public class GetBlockByNameQuery : IRequest<BlockListModel>
    {
        internal string BlockName { get; set; }
        public GetBlockByNameQuery(string blockName)
        {
            BlockName = blockName;
        }
    }
}