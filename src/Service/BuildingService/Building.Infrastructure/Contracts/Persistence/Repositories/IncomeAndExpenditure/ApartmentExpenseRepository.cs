using Building.Application.Contracts.Persistence.Repositories.IncomeAndExpenditure;
using Building.Domain.Entities.IncomeAndExpenditure;
using Building.Infrastructure.Contracts.Persistence.DbSetting;
using Building.Infrastructure.Contracts.Persistence.Repositories.Common;

namespace Building.Infrastructure.Contracts.Persistence.Repositories.IncomeAndExpenditure
{
    public class ApartmentExpenseRepository : BaseRepository<ApartmentExpense>, IApartmentExpenseRepository
    {
        public ApartmentExpenseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
