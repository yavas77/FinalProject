using MediatR;

namespace Building.Application.Features.Queries.Buildings.GetBlocks
{
    public class GetBlockByIdQuery : IRequest<BlockListModel>
    {
        internal int Id { get; set; }
        public GetBlockByIdQuery(int id)
        {
            Id = id;
        }
    }
}