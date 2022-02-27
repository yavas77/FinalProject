using AutoMapper;
using Building.Application.Services.Buildings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.Buildings.GetApartments
{
    public class GetApartmentListByStatusQueryHandler : IRequestHandler<GetApartmentListByStatusQuery, List<ApartmentListModel>>
    {
        #region Properties

        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public GetApartmentListByStatusQueryHandler(IApartmentService apartmentService, IMapper mapper)
        {
            _apartmentService = apartmentService;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        public async Task<List<ApartmentListModel>> Handle(GetApartmentListByStatusQuery request, CancellationToken cancellationToken)
        {

            var apartmentList = await _apartmentService.GetAllAsync(x => x.IsDelete == true && x.Status == true && x.IsActive == true, tracking: true, includeList: "Block");
            if (apartmentList is null)
            {
                return null;
            }

            return _mapper.Map<List<ApartmentListModel>>(apartmentList);
        }

        #endregion
    }
}
