using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.IncomeAndExpenditure.DeptPayments
{
    public class UserUpdateBalanceCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public decimal Amounth { get; set; }

        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public UserUpdateBalanceCommand()
        {
            IsActive = true;
            IsActive = true;
        }
    }
}
