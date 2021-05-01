using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstNameEn { get; set; }
        public string LastNameEn { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual bool IsDeleted { get; set; }
        public string IDN { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public long? Gender { get; set; }
        public List<string> UserRoles { get; set; }

        public string Street { get; set; }
        public string City { get; set; }

        public List<string> Roles { get; set; }
        public string Applications { get; set; }

        public string UserName { get; set; }
    }
}
