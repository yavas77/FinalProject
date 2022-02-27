using Building.Application.Contracts.Persistence.Repositories.IncomeAndExpenditure;
using Building.Domain.Entities.IncomeAndExpenditure;
using Building.Infrastructure.Contracts.Persistence.DbSetting;
using Building.Infrastructure.Contracts.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Infrastructure.Contracts.Persistence.Repositories.IncomeAndExpenditure
{
    public class CaseRepository : BaseRepository<Case>, ICaseRepository
    {
        public CaseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
