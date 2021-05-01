using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class PagedData<T>
    {
        public IList<T> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
