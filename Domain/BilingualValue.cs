using System.Collections.Generic;
using System.Threading;

namespace Domain
{
    public class BilingualValue : ValueObject
    {
        public BilingualValue() { 
        }

        public BilingualValue(string arabicValue, string englishValue)
        {
            ArabicValue = arabicValue;
            EnglishValue = englishValue;
        }

        public virtual string ArabicValue { get; set; }
        public virtual string EnglishValue { get; set; }
        public virtual string Value
        {
            get { return Thread.CurrentThread.CurrentCulture.Name == "en" ? EnglishValue : ArabicValue; }
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ArabicValue;
            yield return EnglishValue;
        }
    }
}
