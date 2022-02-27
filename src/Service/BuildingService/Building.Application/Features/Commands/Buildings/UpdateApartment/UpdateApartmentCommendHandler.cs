using AutoMapper;
using Building.Application.Model.Common;
using Building.Application.Services.Buildings;
using Building.Domain.Entities.Building;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.Buildings.UpdateApartment
{
    public class UpdateApartmentCommendHandler : IRequestHandler<UpdateApartmentCommand, EntityResult>
    {
        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;


        public UpdateApartmentCommendHandler(IApartmentService apartmentService, IMapper mapper)
        {
            _apartmentService = apartmentService;
            _mapper = mapper;
        }

        public async Task<EntityResult> Handle(UpdateApartmentCommand request, CancellationToken cancellationToken)
        {

            //Veritabanında kayıt olup olmadığının kontrol edilmesi
            var entityInDb = await _apartmentService.GetAsync(x => x.Id == request.Id && x.IsDelete == true);

            if (entityInDb == null)
            {
                var entityResult = new EntityResult { Message = "Güncellenmek istenen {Daire} bulunamadı!", Success = true };
                return entityResult;
            }


            var entityApatment = _mapper.Map<Apartment>(request);

            var result = await _apartmentService.UpdateAsync(entityApatment);


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
    }
}
