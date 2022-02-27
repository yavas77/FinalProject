using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Application.Features.Queries.IncomeAndExpenditure
{
    public class GetApartmentExpenseByIdQuery : IRequest<ApartmentExpenseListModel>
    {
        internal int Id { get; set; }
        public GetApartmentExpenseByIdQuery(int id)
        {
            Id = id;
        }
    }
}