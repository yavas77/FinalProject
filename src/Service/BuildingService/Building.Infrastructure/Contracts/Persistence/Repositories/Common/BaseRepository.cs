using Building.Application.Contracts.Persistence.Repositories.Commons;
using Building.Domain.Entities.Commons;
using Building.Infrastructure.Contracts.Persistence.DbSetting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Building.Infrastructure.Contracts.Persistence.Repositories.Common
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        }

        #region Select

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, bool tracking = true, params string[] includeList)
        {
            IQueryable<TEntity> _dbSet = _dbContext.Set<TEntity>();

            if (tracking)
                _dbSet = _dbSet.AsNoTracking();

            if (includeList.Length > 0)
            {
                foreach (var item in includeList)
                {
                    _dbSet = _dbSet.Include(item);
                }
            }

            return filter == null
                    ? await _dbSet.ToListAsync()
                    : await _dbSet.Where(filter).ToListAsync();
        }


        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool tracking = true, params string[] includeList)
        {
            IQueryable<TEntity> _dbSet = _dbContext.Set<TEntity>();

            if (tracking)
                _dbSet = _dbSet.AsNoTracking();

            if (includeList.Length > 0)
            {
                foreach (var item in includeList)
                {
                    _dbSet = _dbSet.Include(item);
                }
            }

            return await _dbSet.SingleOrDefaultAsync(filter);
        }

        public async Task<TEntity> GetByIdAsync(int id, bool tracking = true, params string[] includeList)
        {

            IQueryable<TEntity> _dbSet = _dbContext.Set<TEntity>();

            if (tracking)
                _dbSet = _dbSet.AsNoTracking();

            if (includeList.Length > 0)
            {
                foreach (var item in includeList)
                {
                    _dbSet = _dbSet.Include(item);
                }
            }

            return await _dbSet.SingleOrDefaultAsync(x => x.Id == id);
        }

        #endregion

        #region Add

        public async Task<int> AddAsync(TEntity entity)
        {
            #region DefaultValue

            entity.IsActive = true;
            entity.IsDelete = true;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;

            #endregion

            await _dbContext.AddAsync(entity);

            //try
            //{
            return await _dbContext.SaveChangesAsync();
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.StackTrace);
            //}

        }

        public async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbContext.AddRangeAsync(entities);
            return await _dbContext.SaveChangesAsync();
        }

        #endregion

        #region Delete

        public async Task<int> DeleteAsync(TEntity entity)
        {
            _dbContext.Remove(entity);
            return await _dbContext.SaveChangesAsync();

        }

        public async Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.RemoveRange(entities);
            return await _dbContext.SaveChangesAsync();
        }

        #endregion

        #region Update

        public async Task<int> UpdateAsync(TEntity entity)
        {
            #region DefaultValue

            entity.UpdatedDate = DateTime.Now;

            #endregion
            
            var entry = _dbContext.Entry(entity);
            
            entry.State = EntityState.Modified;

            return await _dbContext.SaveChangesAsync();



        }

        public async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.UpdateRange(entities);
            return await _dbContext.SaveChangesAsync();
        }

        #endregion
    }
}
