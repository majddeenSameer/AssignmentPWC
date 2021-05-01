using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _dataContext;

        public UnitOfWork(Context dataContext)
        {
            _dataContext = dataContext;
        }

        public void SaveChanges()
        {
            _dataContext.SaveChanges();
        }

        public void Rollback()
        {
            _dataContext.Dispose();
        }
    }
}
