using AutoMapper;
using Building.Application.Model.Common;
using Building.Application.Services.Buildings;
using Building.Domain.Entities.Building;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.Buildings.AddApartment
{
    public class AddApartmentCommandHandler : IRequestHandler<AddApartmentCommand, EntityResult>
    {
        #region Properties

        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public AddApartmentCommandHandler(IApartmentService apartmentService, IMapper mapper)
        {
            _apartmentService = apartmentService;
            _mapper = mapper;
        }

        #endregion



        #region Methods

        public async Task<EntityResult> Handle(AddApartmentCommand request, CancellationToken cancellationToken)
        {
            var entityApartment= _mapper.Map<Apartment>(request);

            var result = await _apartmentService.AddAsync(entityApartment);

            if (result > 0)
            {
                var entityResult = new EntityResult { Message = "İşlem başarıyla gerçekleşti!", Success = true };
                return entityResult;
            }
            else
            {
                var entityResult = new EntityResult
                {
                    Success = false,
                    Message = "İşlem başarısız oldu!",
                    Errors = new System.Collections.Generic.List<string> { "Beklenmedik bir hata oluştu!" }
                };
                return entityResult;


            }
        }

        #endregion 
    }
}