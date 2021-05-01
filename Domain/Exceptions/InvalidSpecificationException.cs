using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    [Serializable]
    public class InvalidSpecificationException : Exception
    {
        public InvalidSpecificationException(string message)
            : base(message)
        {
        }
    }
}
