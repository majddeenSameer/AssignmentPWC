using System.Collections.Generic;
using Domain.Lookups;

namespace Domain.DomainModels
{
    public class Address : ValueObject
    {
        public virtual string Street { get; private set; }

        public virtual string City { get; private set; }

        public virtual Country Country { get; private set; }

        public virtual long? CountryId { get; set; }

        protected Address()
        {
            // required by EF
        }
        public Address(string street, string city, string state, long countryId, string zipCode ,string details , long? emirateId)
        {
            Street = street;
            City = city;
            CountryId = countryId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Street;
            yield return City;
            yield return CountryId;
        }
    }
}
