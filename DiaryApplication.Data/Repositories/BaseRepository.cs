using DiaryApplication.Data.Extensions;
using DiaryApplicationAPI.Models.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DiaryApplication.Data.Repositories
{
    public class BaseRepository<T, D> where T : class, IIdentifier where D : DbContext
    {


        #region constants
        public const int DEFAULTPAGESIZE = 50;
        public const int DEFAULTPAGE = 1;
        #endregion

        #region fields
        private D _context;

        #endregion

        #region constructor
        public BaseRepository(D context)
        {
            _context = context;
        }
        #endregion

        #region protected properties
        protected D Context
        {
            get
            {
                return _context;
            }
        }
        #endregion

        #region public methods


        public void DeleteAll<T>() where T : class
        {
            _context.Set<T>().RemoveRange(_context.Set<T>());
        }

        public T GetById(int id, params Expression<Func<T, object>>[] includes)
        {
            var queryable = _context.Set<T>().Where(q => q.Id.Equals(id));

            queryable = queryable.WithIncludes(includes);

            return queryable.FirstOrDefault();
        }

        public T GetByIdReadOnly(int id, params Expression<Func<T, object>>[] includes)
        {
            var queryable = _context.Set<T>().Where(q => q.Id.Equals(id));

            queryable = queryable.WithIncludes(includes);

            return queryable.AsNoTracking().FirstOrDefault();
        }

        public virtual T InsertOrUpdate(T entity)
        {
            if (entity.Id.Equals(0))
            {
                if (entity is IAuditInfo)
                {
                    ((IAuditInfo)entity).CreatedOn = DateTime.Now;
                    ((IAuditInfo)entity).ModifiedOn = DateTime.Now;
                }

                _context.Set<T>().Add(entity);
            }
            else
            {
                if (entity is IAuditInfo)
                {
                    ((IAuditInfo)entity).ModifiedOn = DateTime.Now;
                }

                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }

            return (T)entity;
        }

        public virtual List<T> GetAll(Func<IQueryable<T>
            , IOrderedQueryable<T>> orderBy = null, int? page = null, int? pageSize = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            query = query.WithIncludes(includes);

            if (orderBy != null)
            {
                var orderByQuery = orderBy(query);
                if (orderByQuery == null)
                {
                    query = query.OrderByDescending(q => q.Id);
                }
                else
                {
                    query = orderByQuery;
                }
            }

            if (page.HasValue && pageSize.HasValue)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            else
            {
                query = query.Take(DEFAULTPAGESIZE);
            }

            return query.ToList();
        }

        public virtual List<T> GetAllReadOnly(Func<IQueryable<T>
           , IOrderedQueryable<T>> orderBy = null, int? page = null, int? pageSize = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            query = query.WithIncludes(includes);

            if (orderBy != null)
            {
                var orderByQuery = orderBy(query);
                if (orderByQuery == null)
                {
                    query = query.OrderByDescending(q => q.Id);
                }
                else
                {
                    query = orderByQuery;
                }
            }

            if (page.HasValue && pageSize.HasValue)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            else
            {
                query = query.Take(DEFAULTPAGESIZE);
            }

            return query.AsNoTracking().ToList();
        }

        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            _context.Set<T>().Remove(entity);
        }

        public void Delete(int id)
        {
            if (id.Equals(0)) throw new ArgumentNullException("id");

            T entity = GetById(id);
            Delete(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");

            _context.Set<T>().RemoveRange(entities);
        }

        public virtual int Count(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }

        public virtual List<T> Find(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>
            , IOrderedQueryable<T>> orderBy = null, int? page = null,
            int? pageSize = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            //int totalCount = query.Count();

            query = query.WithIncludes(includes);

            if (orderBy != null)
            {
                var orderByQuery = orderBy(query);
                if (orderByQuery == null)
                {
                    query = query.OrderByDescending(q => q.Id);
                }
                else
                {
                    query = orderByQuery;
                }
            }

            //int filteredCount = query.Count();

            if (page.HasValue && pageSize.HasValue)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            else
            {
                query = query.Take(DEFAULTPAGESIZE);
            }

            return query.ToList();
        }

        public virtual List<T> FindReadOnly(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>
           , IOrderedQueryable<T>> orderBy = null, int? page = null,
           int? pageSize = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            //int totalCount = query.Count();

            query = query.WithIncludes(includes);

            if (orderBy != null)
            {
                var orderByQuery = orderBy(query);
                if (orderByQuery == null)
                {
                    query = query.OrderByDescending(q => q.Id);
                }
                else
                {
                    query = orderByQuery;
                }
            }

            //int filteredCount = query.Count();

            if (page.HasValue && pageSize.HasValue)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            else
            {
                query = query.Take(DEFAULTPAGESIZE);
            }

            return query.AsNoTracking().ToList();
        }
        public virtual List<T> FindDistinct(Expression<Func<T, bool>> filter = null, Func<T, object> distinctBy = null, Func<IQueryable<T>
            , IOrderedQueryable<T>> orderBy = null, int? page = null,
            int? pageSize = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query.WithIncludes(includes);

            if (distinctBy != null)
            {
                query = query.DistinctBy<T, object>(distinctBy).AsQueryable();
            }

            if (orderBy != null)
            {
                var orderByQuery = orderBy(query);
                if (orderByQuery == null)
                {
                    query = query.OrderByDescending(q => q.Id);
                }
                else
                {
                    query = orderByQuery;
                }
            }

            if (page.HasValue && pageSize.HasValue)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            else
            {
                query = query.Take(DEFAULTPAGESIZE);
            }

            return query.ToList();
        }

        public virtual List<T> FindDistinctReadOnly(Expression<Func<T, bool>> filter = null, Func<T, object> distinctBy = null, Func<IQueryable<T>
            , IOrderedQueryable<T>> orderBy = null, int? page = null,
            int? pageSize = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query.WithIncludes(includes);

            if (distinctBy != null)
            {
                query = query.DistinctBy<T, object>(distinctBy).AsQueryable();
            }

            if (orderBy != null)
            {
                var orderByQuery = orderBy(query);
                if (orderByQuery == null)
                {
                    query = query.OrderByDescending(q => q.Id);
                }
                else
                {
                    query = orderByQuery;
                }
            }

            if (page.HasValue && pageSize.HasValue)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            else
            {
                query = query.Take(DEFAULTPAGESIZE);
            }

            return query.AsNoTracking().ToList();
        }

    


        public virtual IQueryable<T> FindByQueryable(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query.WithIncludes(includes);

            return query;
        }


        public virtual T FindOne(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query.WithIncludes(includes);

            return (T)query.FirstOrDefault();
        }

        public virtual T FindOneReadOnly(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query.WithIncludes(includes);

            return (T)query.AsNoTracking().FirstOrDefault();
        }

        public virtual void SetStateDeleted(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;

        }

        #endregion

    }
}
