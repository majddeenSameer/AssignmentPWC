using Mapster;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Domain.Interfaces;
using DTO;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    public abstract class GenericControllerBase<TEntity, TDto, TResultDto> : ControllerBase
     where TEntity : EntityBase, IAggregateRoot
        where TDto : class, IDto, new()
        where TResultDto : class, IDto, new()
    {
        protected readonly IReadAsyncRepository<TEntity> _repository;
        protected ISpecification<TEntity> _searchSpecification;
        protected readonly IDomainService<TEntity> _domainService;
        protected IIdentityService _identityService { get; set; }

        public GenericControllerBase(IReadAsyncRepository<TEntity> repository, IDomainService<TEntity> domainService, ISpecificationFactory specificationFactory, IIdentityService identityService)
        {
            _repository = repository;
            _identityService = identityService;
            _searchSpecification = specificationFactory.Create<TEntity>();
            _domainService = domainService;
        }


        // GET: api/[controller]
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TResultDto>>> Get()
        {
            var result = await _repository.ListAllAsync<TResultDto>();
            return result;
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TResultDto>> Get(long id)
        {
            var result = await _repository.GetByIdAsync<TResultDto>(id);
            if (result == null)
                return NotFound();

            return result;
        }

        // PUT: api/[controller]/5
        [HttpPut]
        public virtual async Task<IActionResult> Put(TDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.Id.Value);
            dto.Adapt(entity);
            await _domainService.UpdateAsync(entity);
            return Ok();
        }

        // POST: api/[controller]
        [HttpPost]
        public virtual async Task<ActionResult<TResultDto>> Post(TDto dto)
        {
            TEntity entity = dto.Adapt<TEntity>();
            await _domainService.AddAsync(entity);

            return await Get(entity.Id);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            await _domainService.DeleteAsync(entity);

            return Ok();
        }

        [HttpPost]
        [Route("UpdateRange")]
        public virtual async Task<IEnumerable<TResultDto>> UpdateRange(IEnumerable<TDto> dto)
        {
            IEnumerable<TEntity> entities = dto.Adapt<IEnumerable<TEntity>>();
            var UpdatedEntities = await _domainService.UpdateRangeAsync(entities);
            IEnumerable<TResultDto> UpdateResult = UpdatedEntities.Adapt<IEnumerable<TResultDto>>();
            return UpdateResult;

        }

    }
}
