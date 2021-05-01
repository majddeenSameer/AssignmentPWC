using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using Domain.Interfaces;

namespace Domain.DomainModels
{
    public class User : IdentityUser, IAggregateRoot
    {
        public virtual string FirstNameEn { get; set; }
        public virtual string LastNameEn { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string Password { get; set; }
        public virtual string IDN { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public virtual long? Gender { get; set; }
        public virtual IList<UserRole> UserRoles { get; set; }
        public virtual IList<UserApplication> UserApplications { get; set; }
        public virtual string Street { get;  set; }
        public virtual string City { get;  set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual bool IsDeleted { get; set; }

    }
}
