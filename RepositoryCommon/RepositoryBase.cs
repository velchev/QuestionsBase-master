namespace RepositoryCommon
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Core;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected DbSet<TEntity> CurrentEntity;
        protected DbContext Context;
        protected IDbContextFactory<DbContext> DbContextFactory;

        public RepositoryBase(IDbContextFactory<DbContext> dbContextFactory)
        {
            DbContextFactory = dbContextFactory;
            Context = dbContextFactory.Create();
            CurrentEntity = Context.Set<TEntity>();
        }

        public void Save()
        {
            try
            {
                Context.ChangeTracker.DetectChanges();
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving entity", ex);
            }

        }

        public TEntity AddAndSave(TEntity entity)
        {
            CurrentEntity.Add(entity);
            Save();
            return entity;
        }

        public TEntity Add(TEntity entity)
        {
            CurrentEntity.Add(entity);
            return entity;
        }

        public bool Update(TEntity entity)
        {
            try
            {
                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    CurrentEntity.Attach(entity);
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Faild to update entity.", ex);
            }


            Context.Entry(entity).State = EntityState.Modified;
         
            return true;
        }

        public void UpdateApplyCurrentValues(TEntity entityUpdate, Expression<Func<TEntity, bool>> filter = null,
                                             string includeProperties = "")
        {
            try
            {
                var entity = Get(filter, includeProperties: includeProperties).FirstOrDefault();
                CurrentEntity.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;

                object originalItem;
                var key = GetEntityKey(entity);

                if (((IObjectContextAdapter) Context).ObjectContext.TryGetObjectByKey(key, out originalItem))
                {
                    ((IObjectContextAdapter) Context).ObjectContext.ApplyCurrentValues(key.EntitySetName, entityUpdate);
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Fail to update entity.", ex);
            }
        }

        public bool Delete(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                CurrentEntity.Attach(entity);
            }

            CurrentEntity.Remove(entity);

            return true;
        }

        public void Delete(object id)
        {
            var entityToDelete = CurrentEntity.Find(id);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                        string includeProperties = "")
        {
            IQueryable<TEntity> query = CurrentEntity;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Aggregate(query,
                                                                                                          (current,
                                                                                                           includeProperty)
                                                                                                          =>
                                                                                                          current.
                                                                                                              Include(
                                                                                                                  includeProperty));

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter = null,
                                      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                      string includeProperties = "")
        {
            IQueryable<TEntity> query = CurrentEntity;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query,
                                                                                                          (current,
                                                                                                           includeProperty)
                                                                                                          =>
                                                                                                          current.
                                                                                                              Include(
                                                                                                                  includeProperty));

            return orderBy != null ? orderBy(query) : query;
        }

        public TEntity CreateNewEntity()
        {
            return CurrentEntity.Create();
        }

        public ICollection<TEntity> GetAll()
        {
            return CurrentEntity.Select(s => s).ToList();
        }

        public IQueryable<TEntity> GetAllQueryable()
        {
            return CurrentEntity;
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await CurrentEntity.Select(s => s).ToListAsync();
        }

        public TEntity GetById(object id)
        {
            return CurrentEntity.Find(id);
        }

        public Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> filter)
        {
            return CurrentEntity.FirstAsync(filter);
        }

        public void RollBack()
        {
            if (Context != null)
            {
                Context.Dispose();
            }

            Context = DbContextFactory.Create();
        }

        private EntityKey GetEntityKey(TEntity entity)
        {
            var oc = ((IObjectContextAdapter) Context).ObjectContext;

            ObjectStateEntry ose;

            if (entity != null && oc.ObjectStateManager.TryGetObjectStateEntry(entity, out ose))
            {
                return ose.EntityKey;
            }

            return null;
        }

        public bool AddOrUpdate(TEntity entity)
        {
            try
            {
                CurrentEntity.AddOrUpdate(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Couldnot save data.", ex);
            }

        }
    }
}