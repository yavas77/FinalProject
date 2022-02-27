using Building.Application.Model.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.IncomeAndExpenditure.UpdateApartmentExpense
{
    public class UpdateApartmentExpenseCommand : IRequest<EntityResult>
    {
        public int Id { get; set; }
        public string Bill { get; set; }

        public decimal Amount { get; set; }

        public int UserId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public UpdateApartmentExpenseCommand()
        {
            IsActive = true;
            IsDelete = true;
        }
    }
}
