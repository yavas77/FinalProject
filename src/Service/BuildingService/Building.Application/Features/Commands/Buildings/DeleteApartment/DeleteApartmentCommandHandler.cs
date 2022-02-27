using AutoMapper;
using Building.Application.Features.Commands.Buildings.UpdateApartment;
using Building.Application.Model.Common;
using Building.Application.Services.Buildings;
using Building.Domain.Entities.Building;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.Buildings.DeleteApartment
{
    public class DeleteApartmentCommandHandler : IRequestHandler<DeleteApartmentCommand, EntityResult>
    {
        #region Properties

        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Constructor

        public DeleteApartmentCommandHandler(IApartmentService apartmentService, IMapper mapper, IMediator mediator)
        {
            _apartmentService = apartmentService;
            _mapper = mapper;
            _mediator = mediator;
        }

        #endregion

        #region Methods

        public async Task<EntityResult> Handle(DeleteApartmentCommand request, CancellationToken cancellationToken)
        {

            //Veritabanında kayıt olup olmadığının kontrol edilmesi
            var entityInDb = await _apartmentService.GetAsync(x => x.Id == request.Id);

            if (entityInDb == null)
            {
                var entityResult = new EntityResult { Message = "Silinmek istenen {Fatura} bulunamadı!", Success = true };
                return entityResult;
            }

            int result = 0;
            var apartmentInDb = _mapper.Map<Apartment>(request);
            if (apartmentInDb.Status == true)
            {
                result = await _apartmentService.RemoveAsync(apartmentInDb);
            }
            else
            {
                var updateApartmentCommand = _mapper.Map<UpdateApartmentCommand>(apartmentInDb);
                //updateApartmentCommand.
                await _mediator.Send(updateApartmentCommand);
            }



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