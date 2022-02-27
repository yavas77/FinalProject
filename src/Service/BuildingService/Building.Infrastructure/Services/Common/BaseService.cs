using Building.Application.Contracts.Persistence.Repositories.Commons;
using Building.Application.Services.Commons;
using Building.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Building.Infrastructure.Services.Common
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        #region Insert

        public async Task<int> AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return await _repository.AddAsync(entity);
        }

        public async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            return await _repository.AddRangeAsync(entities);
        }

        #endregion

        #region Select

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool tracking = true, params string[] includeList)
        {
            return await _repository.GetAsync(filter, tracking, includeList);
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, bool tracking = true, params string[] includeList)
        {
            return await _repository.GetAllAsync(filter, tracking, includeList);
        }

        public async Task<TEntity> GetByIdAsync(int entityId, bool tracking = true, params string[] includeList)
        {
            return await _repository.GetByIdAsync(entityId, tracking, includeList);
        }

        #endregion

        #region Delete

        public async Task<int> RemoveAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return await _repository.DeleteAsync(entity);
        }

        public async Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            return await _repository.AddRangeAsync(entities);
        }

        #endregion

        #region Update

        public async Task<int> UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return await _repository.UpdateAsync(entity);
        }

        public async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            return await _repository.UpdateRangeAsync(entities);
        }

        #endregion
    }
}
