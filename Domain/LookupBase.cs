using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Domain
{
    public abstract class LookupBase : EntityBase
    {

        public LookupBase()
        {

        }
        protected LookupBase(long id)
        {
            Id = id;
        }

        public string Code { get; set; }
        public virtual string DescriptionArabic { get; set; }
        public virtual string DescriptionEnglish { get; set; }
        public virtual string Description
        {
            get { return $"{(!string.IsNullOrEmpty(Code) ? $"({Code})" : "")}{(Thread.CurrentThread.CurrentCulture.Name == "en" ? DescriptionEnglish : DescriptionArabic)}"; }
        }

        public virtual DateTime? ValidFrom { get; set; }
        public virtual DateTime? ValidTo { get; set; }

        public override string ToString() => Description;

        public override bool Equals(object obj)
        {
            var otherValue = obj as LookupBase;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public int CompareTo(object other) => Id.CompareTo(((LookupBase)other).Id);
    }
}
