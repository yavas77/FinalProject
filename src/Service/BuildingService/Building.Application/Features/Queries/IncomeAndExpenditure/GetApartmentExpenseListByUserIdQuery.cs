using MediatR;
using System.Collections.Generic;

namespace Building.Application.Features.Queries.IncomeAndExpenditure
{
    public class GetApartmentExpenseListByUserIdQuery : IRequest<List<ApartmentExpenseListModel>>
    {
        internal int UserId { get; set; }
        public GetApartmentExpenseListByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}