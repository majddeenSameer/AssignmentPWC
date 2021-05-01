using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain;
using Domain.Interfaces;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
    public class LookupsRepository : ILookupsRepository
    {
        protected readonly Context _dbContext;

        public LookupsRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync<T>(long id) where T : LookupBase
        {
            return await _dbContext.Set<T>().Where(e => e.Id == id).SingleAsync();
        }

        public List<T> ListAll<T>() where T : LookupBase
        {
            return _dbContext.Set<T>().Where(x => (!x.ValidFrom.HasValue || x.ValidFrom <= DateTime.Now) && (!x.ValidTo.HasValue || x.ValidTo >= DateTime.Now)).ToList();
        }

        public T Get<T>(Expression<Func<T, bool>> predicate) where T : LookupBase
        {
            return _dbContext.Set<T>().FirstOrDefault(predicate);
        }

        protected virtual IQueryable<T> Query<T>() where T : LookupBase
        {
            return _dbContext.Set<T>().Where(x => (!x.ValidFrom.HasValue || x.ValidFrom <= DateTime.Now) && (!x.ValidTo.HasValue || x.ValidTo >= DateTime.Now)).AsQueryable<T>();
        }
        public async Task<int> CountAsync<T>(ISpecification<T> specification) where T : LookupBase
        {
            var query = _dbContext.Set<T>().Where(x => (!x.ValidFrom.HasValue || x.ValidFrom <= DateTime.Now) && (!x.ValidTo.HasValue || x.ValidTo >= DateTime.Now)).AsQueryable();

            // modify the IQueryable using the specification's criteria expression
            if (specification.Predicate != null)
            {
                query = query.Where(specification.Predicate);
            }

            return await query.CountAsync();
        }

        public async Task<PagedData<T>> PageAsync<T>(ISpecification<T> specification, int pageIndex, int pageSize) where T : LookupBase
        {
            var pagedData = new PagedData<T>();

            var query = specification.Predicate != null ? specification.Prepare(Query<T>()) : Query<T>();
            query = query.Where(x => (!x.ValidFrom.HasValue || x.ValidFrom <= DateTime.Now) && (!x.ValidTo.HasValue || x.ValidTo >= DateTime.Now));

            var queryProjected = query.Skip(pageIndex * pageSize).Take(pageSize);
            pagedData.TotalCount = await CountAsync(specification);
            pagedData.Data = await queryProjected.ProjectToType<T>().ToListAsync();
            return pagedData;
        }

        public List<T> Search<T>(string text) where T : LookupBase
        {
            return _dbContext.Set<T>()
                .Where(x => (!x.ValidFrom.HasValue || x.ValidFrom <= DateTime.Now) && (!x.ValidTo.HasValue || x.ValidTo >= DateTime.Now)
                && (string.IsNullOrEmpty(text) || x.DescriptionArabic.Contains(text) || x.DescriptionEnglish.Contains(text))).ToList();
        }


        public async Task<List<T>> SearchAsync<T>(string text, ISpecification<T> specification) where T : LookupBase
        {
            var query = specification.Predicate != null ? specification.Prepare(Query<T>()) : Query<T>();
            query = query.Where(x => (!x.ValidFrom.HasValue || x.ValidFrom <= DateTime.Now) && (!x.ValidTo.HasValue || x.ValidTo >= DateTime.Now));

            return await query.ProjectToType<T>().ToListAsync();
        }

       
    }
}