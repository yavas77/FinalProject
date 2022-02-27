using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.IncomeAndExpenditure.GetCase
{
	public class GetAllCaseQuery : IRequest<List<CaseListModel>>
    {
        public GetAllCaseQuery()
        {

        }
    }
}