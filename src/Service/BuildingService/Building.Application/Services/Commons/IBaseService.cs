using Building.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Building.Application.Services.Commons
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        #region Select
        Task<TEntity> GetByIdAsync(int id, bool tracking = true, params string[] includeList);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, bool tracking = true, params string[] includeList);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool tracking = true, params string[] includeList);
        #endregion

        #region Insert

        Task<int> AddAsync(TEntity entity);

        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);

        #endregion

        #region Update

        Task<int> UpdateAsync(TEntity entity);
        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities);

        #endregion

        #region Delete

        Task<int> RemoveAsync(TEntity entity);

        Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities);

        #endregion
    }
}
