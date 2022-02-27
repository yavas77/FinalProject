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
    public class CaseService : BaseService<Case>, ICaseService
    {
        private readonly IBaseRepository<Case> _repository;
        public CaseService(IBaseRepository<Case> repository) : base(repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
    }
}
