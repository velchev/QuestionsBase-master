namespace RepositoryCommon
{
    using System.Linq.Expressions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepositoryBase<TEntity>
    {
        void Save();

        TEntity Add(TEntity entity);

        TEntity AddAndSave(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(TEntity entity);

        void Delete(object id);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        void UpdateApplyCurrentValues(TEntity entityUpdate, Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");

        ICollection<TEntity> GetAll();

        IQueryable<TEntity> GetAllQueryable();

        Task<ICollection<TEntity>> GetAllAsync();

        TEntity GetById(object id);

        Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> filter);

        void RollBack();

        TEntity CreateNewEntity();

        bool AddOrUpdate(TEntity entity);
    }
}