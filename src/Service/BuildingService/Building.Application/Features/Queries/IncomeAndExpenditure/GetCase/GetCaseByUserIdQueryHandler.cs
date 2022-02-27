using AutoMapper;
using Building.Application.Services.IncomeAndExpenditure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.IncomeAndExpenditure.GetCase
{
	public class GetCaseByUserIdQueryHandler : IRequestHandler<GetCaseByUserIdQuery, List<CaseListModel>>
    {
        #region Properties

        private readonly ICaseService _caseService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public GetCaseByUserIdQueryHandler(ICaseService caseService, IMapper mapper)
        {
            _caseService = caseService;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        #endregion

        #region Methods

        public async Task<List<CaseListModel>> Handle(GetCaseByUserIdQuery request, CancellationToken cancellationToken)
        {
            var caseList = await _caseService.GetAllAsync(x => x.UserId == request.UserId && x.IsDelete == true, tracking: true, "User", "User.Apartment.Block");
            if (caseList is null)
            {
                return null;
            }

            return _mapper.Map<List<CaseListModel>>(caseList);
        }

        #endregion
    }
}
