using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.DomainModels;
using DTO;

namespace Domain.Interfaces
{
    public interface IIntegrationRepository
    {
        
        Task<List<ProjectionType>> ListAllAsync<ProjectionType>(int index);
        Task <List<User>> ListAllAsync(int index);

    }
}
