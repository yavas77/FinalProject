using Building.Application.Model.Common;
using Building.Domain.Entities.IncomeAndExpenditure;
using MediatR;

namespace Building.Application.Features.Commands.IncomeAndExpenditure.AddCase
{
    public class AddCaseCommand : IRequest<EntityResult>
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public PaymentType Type { get; set; }
    }
}
