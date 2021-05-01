using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILookupsRepository
    {
        Task<T> GetByIdAsync<T>(long id) where T : LookupBase;
        List<T> ListAll<T>() where T : LookupBase;
        List<T> Search<T>(string text) where T : LookupBase;
        Task<List<T>> SearchAsync<T>(string text , ISpecification<T> specification) where T : LookupBase;
        T Get<T>(Expression<Func<T, bool>> predicate) where T : LookupBase;
        Task<PagedData<T>> PageAsync<T>(ISpecification<T> specification, int pageIndex, int pageSize) where T : LookupBase;
    }
}
