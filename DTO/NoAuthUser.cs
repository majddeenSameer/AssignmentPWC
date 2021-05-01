using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class NoAuthUserDto : IDto
    {
        public string FirstNameEn { get; set; }
        public string LastNameEn { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public LookupDto Gender { get; set; }
        public  string Street { get;  set; }
        public  string City { get;  set; }
        public LookupDto Country { get;  set; }
        public LookupDto UserType { get; set; }
        public long? Id { get; set; }
    }
}
