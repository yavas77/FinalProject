using Building.Application.Contracts.Persistence.Repositories.Commons;
using Building.Application.Services.Buildings;
using Building.Domain.Entities.Building;
using Building.Infrastructure.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building.Infrastructure.Services.Building
{
    public class Blockservice : BaseService<Block>, IBlockService
    {
        private readonly IBaseRepository<Block> _repository;

        public Blockservice(IBaseRepository<Block> repository) : base(repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
    }
}
