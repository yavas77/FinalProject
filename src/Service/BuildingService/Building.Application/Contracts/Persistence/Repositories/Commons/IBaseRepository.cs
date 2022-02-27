using Building.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Building.Application.Contracts.Persistence.Repositories.Commons
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {

        #region Select

        public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, bool tracking=true, params string[] includeList);
        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool tracking = true, params string[] includeList);
        public Task<TEntity> GetByIdAsync(int id, bool tracking = true, params string[] includeList);

        #endregion

        #region Add

        public Task<int> AddAsync(TEntity entity);
        public Task<int> AddRangeAsync(IEnumerable<TEntity> entities);

        #endregion

        #region Update

        public Task<int> UpdateAsync(TEntity entity);
        public Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities); 

        #endregion

        #region Delete

        public Task<int> DeleteAsync(TEntity entity);


        public Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities);

        #endregion

    }
}