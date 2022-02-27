using Building.Application.Contracts.Persistence.Repositories.Commons;
using Building.Application.Services.IncomeAndExpenditure;
using Building.Domain.Entities.IncomeAndExpenditure;
using Building.Infrastructure.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Infrastructure.Services.IncomeAndExpenditure
{
    public class ApartmentExpenseService : BaseService<ApartmentExpense>, IApartmentExpenseService
    {
        private readonly IBaseRepository<ApartmentExpense> _repository;

        public ApartmentExpenseService(IBaseRepository<ApartmentExpense> repository) : base(repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
    }
}
