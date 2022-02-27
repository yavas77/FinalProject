using AutoMapper;
using Building.Application.Services.IncomeAndExpenditure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.IncomeAndExpenditure
{
    public class GetApartmentExpenseListByUserIdQueryHandler : IRequestHandler<GetApartmentExpenseListByUserIdQuery, List<ApartmentExpenseListModel>>
    {
        #region Properties

        private readonly IApartmentExpenseService _apartmentExpenseService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public GetApartmentExpenseListByUserIdQueryHandler(IApartmentExpenseService apartmentExpenseService, IMapper mapper)
        {
            _apartmentExpenseService = apartmentExpenseService;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        #endregion

        #region Methods

        public async Task<List<ApartmentExpenseListModel>> Handle(GetApartmentExpenseListByUserIdQuery request, CancellationToken cancellationToken)
        {
            var apartmentExpenseList = await _apartmentExpenseService.GetAllAsync(x => x.UserId == request.UserId && x.IsDelete == true, tracking: true,"User");
            if (apartmentExpenseList is null)
            {
                return null;
            }

            return _mapper.Map<List<ApartmentExpenseListModel>>(apartmentExpenseList);
        }

        #endregion
    }
}
