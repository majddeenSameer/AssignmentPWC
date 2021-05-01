using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DTO
{
    public class LookupDto
    {
        public long? Id { get; set; }
        public string Code { get; set; }
        public virtual string DescriptionArabic { get; set; }
        public virtual string DescriptionEnglish { get; set; }
        public virtual string Description
        {
            get { return $"{(!string.IsNullOrEmpty(Code) ? $"({Code})" : "")}{(Thread.CurrentThread.CurrentCulture.Name == "en" ? DescriptionEnglish : DescriptionArabic)}"; }
        }
    }
}
