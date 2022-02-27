using AutoMapper;
using Building.Application.Services.IncomeAndExpenditure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.IncomeAndExpenditure
{
    public class GetApartmentExpenseByIdQueryHandler : IRequestHandler<GetApartmentExpenseByIdQuery, ApartmentExpenseListModel>
    {
        #region Properties

        private readonly IApartmentExpenseService _apartmentExpenseService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public GetApartmentExpenseByIdQueryHandler(IApartmentExpenseService apartmentExpenseService, IMapper mapper)
        {
            _apartmentExpenseService = apartmentExpenseService;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        #endregion

        #region Methods

        public async Task<ApartmentExpenseListModel> Handle(GetApartmentExpenseByIdQuery request, CancellationToken cancellationToken)
        {
            var apartmentExpenseList = await _apartmentExpenseService.GetAsync(x => x.IsDelete == true && x.Id == request.Id, tracking: true, "User", "User.Apartment.Block");
            if (apartmentExpenseList is null)
            {
                return null;
            }

            return _mapper.Map<ApartmentExpenseListModel>(apartmentExpenseList);
        }

        #endregion
    }
}
