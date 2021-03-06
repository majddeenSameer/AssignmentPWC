using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DTO;

namespace Domain.Interfaces
{
    public interface IReadAsyncRepository<T> where T : EntityBase, IAggregateRoot
    {
        Task<T> GetByIdAsync(long id);
        Task<ProjectionType> GetByIdAsync<ProjectionType>(long id);

        Task<T> GetByAsync(ISpecification<T> specification);
        Task<ProjectionType> GetByAsync<ProjectionType>(ISpecification<T> specification);

        Task<T> GetByAsync(Expression<Func<T, bool>> criteria);
        Task<ProjectionType> GetByAsync<ProjectionType>(Expression<Func<T, bool>> criteria);

        Task<List<T>> ListAllAsync();
        Task<List<ProjectionType>> ListAllAsync<ProjectionType>();

        Task<List<T>> ListAsync(ISpecification<T> specification);
        Task<List<ProjectionType>> ListAsync<ProjectionType>(ISpecification<T> specification);

        Task<PagedData<T>> PageAsync(ISpecification<T> specification, int pageIndex, int pageSize, string sortBy = null, OrderDirectionEnum sortDirection = OrderDirectionEnum.OrderBy);
        Task<PagedData<ProjectionType>> PageAsync<ProjectionType>(ISpecification<T> specification, int pageIndex, int pageSize, string sortBy = null, OrderDirectionEnum sortDirection = OrderDirectionEnum.OrderBy);

        Task<int> CountAsync(ISpecification<T> spec);

        //Task<T> GetByIdAsyncc(long id);
        //Task<ProjectionType> GetByIdAsyncc<ProjectionType>(long id);

        //Task<T> GetByAsyncc(ISpecification<T> specification);
        //Task<ProjectionType> GetByAsyncc<ProjectionType>(ISpecification<T> specification);

        //Task<T> GetByAsyncc(Expression<Func<T, bool>> criteria);
        //Task<ProjectionType> GetByAsyncc<ProjectionType>(Expression<Func<T, bool>> criteria);


    }
}
