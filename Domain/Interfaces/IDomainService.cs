using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDomainService<T> where T : EntityBase, IAggregateRoot
    {
        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);

        Task DeleteAsync(T entity);
    }
}
