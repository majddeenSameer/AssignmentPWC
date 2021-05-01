using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using Domain.Interfaces;
using Domain.Lookups;

namespace Domain.DomainModels
{
    public class NoAuthUser : AuditableEntityBase, IAggregateRoot
    {
        public virtual string FirstNameEn { get; set; }
        public virtual string LastNameEn { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public virtual Gender Gender { get; private set; }
        public long? GenderId { get; set; }
        public virtual string Street { get; private set; }
        public virtual string City { get; private set; }
        public virtual Country Country { get; private set; }
        public virtual long? CountryId { get; set; }
        public virtual UserType UserType { get; private set; }
        public virtual long? UserTypeId { get; set; }
    }
}
