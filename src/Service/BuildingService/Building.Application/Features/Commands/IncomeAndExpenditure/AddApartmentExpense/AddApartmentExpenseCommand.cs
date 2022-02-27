using Building.Application.Model.Common;
using MediatR;

namespace Building.Application.Features.Commands.IncomeAndExpenditure.AddApartmentExpense
{
    public class AddApartmentExpenseCommand : IRequest<EntityResult>
    {
        public string Bill { get; set; }

        public decimal Amount { get; set; }

        public int UserId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }


    }
}
