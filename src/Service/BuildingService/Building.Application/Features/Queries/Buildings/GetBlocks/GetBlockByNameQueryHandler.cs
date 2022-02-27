using AutoMapper;
using Building.Application.Services.Buildings;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.Buildings.GetBlocks
{
    public class GetBlockByNameQueryHandler : IRequestHandler<GetBlockByNameQuery, BlockListModel>
    {
        #region Properties

        private readonly IBlockService _blockService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public GetBlockByNameQueryHandler(IBlockService blockService, IMapper mapper)
        {
            _blockService = blockService;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        #endregion

        #region Methods

        public async Task<BlockListModel> Handle(GetBlockByNameQuery request, CancellationToken cancellationToken)
        {
            var block = await _blockService.GetAsync(x => x.Name == request.BlockName && x.IsDelete == true);
            if (block is null)
            {
                return null;
            }

            return _mapper.Map<BlockListModel>(block);
        }

        #endregion
    }
}
