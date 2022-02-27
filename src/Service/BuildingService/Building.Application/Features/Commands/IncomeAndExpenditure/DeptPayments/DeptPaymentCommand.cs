using Building.Application.Model.Common;
using Building.Domain.Entities.IncomeAndExpenditure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Application.Features.Commands.IncomeAndExpenditure.DeptPayments
{
    public class DeptPaymentCommand : IRequest<EntityResult>
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string FullName { get; set; }
        public string CardNumber { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int SecurityNumber { get; set; }
        public PaymentType Type { get; set; }
    }
}
