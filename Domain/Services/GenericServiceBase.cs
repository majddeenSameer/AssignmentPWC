using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Services
{
    public class GenericServiceBase<T> : IDomainService<T> where T : EntityBase, IAggregateRoot
    {
        protected readonly IAsyncRepository<T> _repository;
        protected readonly IIdentityService _identityService;

        public GenericServiceBase(IAsyncRepository<T> repository, IIdentityService identityService)
        {
            _repository = repository;
            _identityService = identityService;
        }

        public virtual async Task<T> AddAsync(T entity)
        {

            return await _repository.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public virtual async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            return await _repository.UpdateRangeAsync(entities);
        }

        public virtual async Task DeleteAsync(T entity)
        {
            await _repository.DeleteAsync(entity);
        }
    }
}
