using AutoMapper;
using Building.Application.Services.Buildings;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.Buildings.GetApartments
{
    public class GetApartmentListQueryHandler : IRequestHandler<GetApartmentListQuery, List<ApartmentListModel>>
    {
        #region Properties

        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public GetApartmentListQueryHandler(IApartmentService apartmentService, IMapper mapper)
        {
            _apartmentService = apartmentService;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        public async Task<List<ApartmentListModel>> Handle(GetApartmentListQuery request, CancellationToken cancellationToken)
        {

            var apartmentList = await _apartmentService.GetAllAsync(x => x.IsDelete == true, true, "Block");
            if (apartmentList is null)
            {
                return null;
            }

            return _mapper.Map<List<ApartmentListModel>>(apartmentList);
        }

        #endregion
    }
}
