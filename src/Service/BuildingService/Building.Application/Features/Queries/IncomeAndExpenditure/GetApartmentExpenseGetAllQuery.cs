using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.IncomeAndExpenditure
{
    public class GetApartmentExpenseGetAllQuery : IRequest<List<ApartmentExpenseListModel>>
    {
        public GetApartmentExpenseGetAllQuery()
        {

        }
    }
}