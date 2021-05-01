using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface ISpecificationFactory
    {
        ISpecification<T> Create<T>();
    }
}
