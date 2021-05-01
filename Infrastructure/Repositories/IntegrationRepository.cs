using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain;
using Domain.DomainModels;
using Domain.Interfaces;
using DTO;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
    public class IntegrationRepository : IIntegrationRepository
    {
        protected readonly Context _dbContext;

        public IntegrationRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProjectionType>> ListAllAsync<ProjectionType>(int index)
        {
            var query = _dbContext.Set<User>().AsQueryable<User>();
            return await query.OrderBy(x=>x.Id).Skip(index).Take(5).ProjectToType<ProjectionType>().ToListAsync();
        }

        public async Task<List<User>> ListAllAsync(int index)
        {
            var result = await _dbContext.Set<User>().OrderBy(x => x.Id).Skip(index).Take(20).ToListAsync();
            return result;
        }
    }
}