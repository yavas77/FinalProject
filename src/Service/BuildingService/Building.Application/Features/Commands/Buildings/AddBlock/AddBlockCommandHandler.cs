using AutoMapper;
using Building.Application.Model.Common;
using Building.Application.Services.Buildings;
using Building.Domain.Entities.Building;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.Buildings.AddBlock
{
    public class AddBlockCommandHandler : IRequestHandler<AddBlockCommand, EntityResult>
    {
        #region Properties

        private readonly IBlockService _blockService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public AddBlockCommandHandler(IBlockService blockService, IMapper mapper)
        {
            _blockService = blockService;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        public async Task<EntityResult> Handle(AddBlockCommand request, CancellationToken cancellationToken)
        {
            var entityBlock = _mapper.Map<Block>(request);




            var result = await _blockService.AddAsync(entityBlock);

            if (result > 0)
            {

                var entityResult = new EntityResult { Message = "İşlem başarıyla gerçekleşti!", Success = true };
                return entityResult;
            }
            else
            {
                var entityResult = new EntityResult { Message = "İşlem esnansında hata oluştu! Lütfen yeniden deneyiniz.", Success = false };
                return entityResult;

            }
        }

        #endregion

    }
}
