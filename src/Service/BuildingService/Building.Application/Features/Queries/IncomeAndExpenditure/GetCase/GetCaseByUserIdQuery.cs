using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.IncomeAndExpenditure.GetCase
{
	public class GetCaseByUserIdQuery : IRequest<List<CaseListModel>>
    {
        internal int UserId { get; set; }
        public GetCaseByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}