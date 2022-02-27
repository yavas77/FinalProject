using Building.Application.Contracts.Persistence.Repositories.Buildings;
using Building.Domain.Entities.Building;
using Building.Infrastructure.Contracts.Persistence.DbSetting;
using Building.Infrastructure.Contracts.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Infrastructure.Contracts.Persistence.Repositories.Buildings
{
    public class BlockRepository : BaseRepository<Block>, IBlockRepository
    {
        public BlockRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
