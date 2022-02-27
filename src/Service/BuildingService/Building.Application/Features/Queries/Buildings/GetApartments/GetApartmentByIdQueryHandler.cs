using AutoMapper;
using Building.Application.Services.Buildings;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.Buildings.GetApartments
{
    public class GetApartmentByIdQueryHandler : IRequestHandler<GetApartmentByIdQuery, ApartmentListModel>
    {
        #region Properties

        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public GetApartmentByIdQueryHandler(IApartmentService apartmentService, IMapper mapper)
        {
            _apartmentService = apartmentService;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        public async Task<ApartmentListModel> Handle(GetApartmentByIdQuery request, CancellationToken cancellationToken)
        {

            var apartment = await _apartmentService.GetAsync(x => x.Id == request.ApartmentId && x.IsActive == true && x.IsDelete == true, true, "Block");

            if (apartment is null)
            {
                return null;
            }

            return _mapper.Map<ApartmentListModel>(apartment);
        }

        #endregion
    }
}
