using System;
using System.Collections.Generic;
using System.Text;
using Domain.Interfaces;

namespace Domain
{
    public class SpecificationFactory : ISpecificationFactory
    {
        public ISpecification<T> Create<T>()
        {
            return new NullSpecification<T>();
        }
    }
}
