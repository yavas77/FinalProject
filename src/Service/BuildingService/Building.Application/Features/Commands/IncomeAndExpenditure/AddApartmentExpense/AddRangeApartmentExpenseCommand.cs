using Building.Application.Model.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.IncomeAndExpenditure.AddApartmentExpense
{
    public class AddRangeApartmentExpenseCommand : IRequest<EntityResult>
    {
        public string Bill { get; set; }

        public decimal Amount { get; set; }

        public int UserId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }


    }
}
