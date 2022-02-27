using AutoMapper;
using Building.Application.Contracts.Persistence.Repositories.Commons;
using Building.Application.Model.Common;
using Building.Application.Services.Buildings;
using Building.Domain.Entities.Building;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.Buildings.UpdateBlock
{
    public class UpdateBlockCommandHandler : IRequestHandler<UpdateBlockCommand, EntityResult>
    {
        #region Properties

        private readonly IBlockService _blockService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public UpdateBlockCommandHandler(IBlockService blockService, IMapper mapper)
        {
            _blockService = blockService;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        public async Task<EntityResult> Handle(UpdateBlockCommand request, CancellationToken cancellationToken)
        {


            var entityInDb = await _blockService.GetAsync(x => x.Id == request.Id && x.IsDelete == true);

            if (entityInDb == null)
            {
                var entityResult = new EntityResult { Message = "Güncellenmek istenen Blok bulunamadı!", Success = false };
                return entityResult;
            }


            var entityBlock = _mapper.Map<Block>(request);



            var result = await _blockService.UpdateAsync(entityBlock);

            if (result > 0)
            {

                var entityResult = new EntityResult { Message = "İşlem başarıyla gerçekleşti.", Success = true };
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
