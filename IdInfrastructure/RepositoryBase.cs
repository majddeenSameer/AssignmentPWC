using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain;
using Domain.Interfaces;
using IdInfrastructure.Identity;
using DTO;

namespace IdInfrastructure
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase, IAggregateRoot
    {
        protected readonly IdentityContext _identityContext;


        public RepositoryBase(IdentityContext identityContext)
        {
            _identityContext = identityContext;

        }

        protected virtual IQueryable<T> Query()
        {
            return _identityContext.Set<T>().AsQueryable<T>();
        }

        protected virtual IQueryable<QueryType> Query<QueryType>() where QueryType : EntityBase
        {
            return _identityContext.Set<QueryType>().AsQueryable<QueryType>();
        }

        public virtual async Task<T> GetByIdAsyncc(long id)
        {
            return await _identityContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<ProjectionType> GetByIdAsyncc<ProjectionType>(long id)
        {
            return await _identityContext.Set<T>().Where(e => e.Id == id).ProjectToType<ProjectionType>().SingleAsync();
        }    
        
        public virtual async Task<T> GetByIdAsync(long id)
        {
            return await _identityContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<ProjectionType> GetByIdAsync<ProjectionType>(long id)
        {
            return await _identityContext.Set<T>().Where(e => e.Id == id).ProjectToType<ProjectionType>().SingleAsync();
        }


        public async Task<List<T>> ListAllAsync()
        {
            return await _identityContext.Set<T>().ToListAsync();
        }
        public async Task<List<ProjectionType>> ListAllAsync<ProjectionType>()
        {
            return await _identityContext.Set<T>().ProjectToType<ProjectionType>().ToListAsync();
        }

        public async Task<List<T>> ListAsync(ISpecification<T> specification)
        {
            var query = specification.HasPredicate ? specification.Prepare(Query()) : Query();
            return await query.ToListAsync();
        }

        public async Task<List<ProjectionType>> ListAsync<ProjectionType>(ISpecification<T> specification)
        {
            var query = specification.HasPredicate ? specification.Prepare(Query()) : Query();
            return await query.ProjectToType<ProjectionType>().ToListAsync();
        }


        public async Task<PagedData<T>> PageAsync(ISpecification<T> specification, int pageIndex, int pageSize, string sortBy = null, OrderDirectionEnum sortDirection = OrderDirectionEnum.OrderBy)
        {
            var pagedData = new PagedData<T>();

            var query = specification.HasPredicate ? specification.Prepare(Query()) : Query();

            if (!string.IsNullOrEmpty(sortBy))
            {
                var parameter = Expression.Parameter(typeof(T), "p");
                var property = typeof(T).GetProperty(sortBy);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExpression = Expression.Lambda(propertyAccess, parameter);

                Expression resultExpression = Expression.Call(typeof(Queryable), sortDirection.ToString(), new Type[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(orderByExpression));
                query = query.Provider.CreateQuery<T>(resultExpression);
            }

            var queryProjected = query.Skip(pageIndex * pageSize).Take(pageSize);
            pagedData.TotalCount = await CountAsync(specification);
            pagedData.Data = await queryProjected.ToListAsync();
            return pagedData;
        }

        public async Task<PagedData<ProjectionType>> PageAsync<ProjectionType>(ISpecification<T> specification, int pageIndex, int pageSize, string sortBy = null, OrderDirectionEnum sortDirection = OrderDirectionEnum.OrderBy)
        {
            PagedData<ProjectionType> pagedData = new PagedData<ProjectionType>();

            var query = specification.HasPredicate ? specification.Prepare(Query()) : Query();

            #region Order By

            if (!string.IsNullOrEmpty(sortBy))
            {
                var parameter = Expression.Parameter(typeof(T), "p");
                var property = typeof(T).GetProperty(sortBy);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExpression = Expression.Lambda(propertyAccess, parameter);
                //sortDirection should be OrderBy or OrderByDescending
                Expression resultExpression = Expression.Call(typeof(Queryable), sortDirection.ToString(), new Type[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(orderByExpression));
                query = query.Provider.CreateQuery<T>(resultExpression);
            }

            #endregion Order By

            var queryProjected = query.Skip(pageIndex * pageSize).Take(pageSize).ProjectToType<ProjectionType>();
            pagedData.TotalCount = await CountAsync(specification);
            pagedData.Data = queryProjected.ToList();
            return pagedData;
        }

        public async Task<int> CountAsync(ISpecification<T> specification)
        {
            var query = _identityContext.Set<T>().AsQueryable();

            // modify the IQueryable using the specification's criteria expression
            if (specification.HasPredicate)
            {
                query = query.Where(specification.Predicate);
            }

            return await query.CountAsync();
        }

        public async Task<long> MaxAsync(ISpecification<T> specification)
        {
            var query = _identityContext.Set<T>().AsQueryable();

            // modify the IQueryable using the specification's criteria expression
            if (specification.HasPredicate)
            {
                query = query.Where(specification.Predicate);
            }

            var count = await query.CountAsync();
            if (count > 0)
                return await query.MaxAsync(e => e.Id);
            else
                return 0;
        }

        public async Task<T> AddAsync(T entity)
        {
            //_identityContext.BeginTransaction();
            await _identityContext.Set<T>().AddAsync(entity);
            await _identityContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            //_identityContext.BeginTransaction();
            _identityContext.Entry(entity).State = EntityState.Modified;
            await _identityContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            //_identityContext.BeginTransaction();
            _identityContext.Set<T>().Remove(entity);
            await _identityContext.SaveChangesAsync();
        }

        public async Task<T> GetByAsync(Expression<Func<T, bool>> criteria)
        {
            return await _identityContext.Set<T>().SingleOrDefaultAsync(criteria);
        }

        public async Task<ProjectionType> GetByAsync<ProjectionType>(Expression<Func<T, bool>> criteria)
        {
            return await _identityContext.Set<T>().Where(criteria).ProjectToType<ProjectionType>().SingleAsync();
        }

        public async Task<T> GetByAsync(ISpecification<T> specification)
        {
            var query = specification.HasPredicate ? specification.Prepare(Query()) : Query();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<ProjectionType> GetByAsync<ProjectionType>(ISpecification<T> specification)
        {
            var query = specification.HasPredicate ? specification.Prepare(Query()) : Query();
            return await query.ProjectToType<ProjectionType>().FirstOrDefaultAsync();
        }

        public Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByAsyncc(ISpecification<T> specification)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByAsyncc(Expression<Func<T, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectionType> GetByAsyncc<ProjectionType>(Expression<Func<T, bool>> criteria)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectionType> GetByAsyncc<ProjectionType>(ISpecification<T> specification)
        {
            throw new NotImplementedException();
        }

        public Task<T> AddAsyncc(T entity)
        {
            throw new NotImplementedException();
        }
    }

}
