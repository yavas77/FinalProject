using AutoMapper;
using Building.Application.Services.Buildings;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.Buildings.GetBlocks
{
    public class GetBlockListQueryHandler : IRequestHandler<GetBlockListQuery, List<BlockListModel>>
    {
        #region Properties

        private readonly IBlockService _blockService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public GetBlockListQueryHandler(IBlockService blockService, IMapper mapper)
        {
            _blockService = blockService;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        public async Task<List<BlockListModel>> Handle(GetBlockListQuery request, CancellationToken cancellationToken)
        {

            var blockList = await _blockService.GetAllAsync(x => x.IsDelete == true);
            if (blockList is null)
            {
                return null;
            }

            return _mapper.Map<List<BlockListModel>>(blockList);
        } 

        #endregion
    }
}
