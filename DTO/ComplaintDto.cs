using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ComplaintDto : IDto
    {
        public NoAuthUserDto NoAuthUser { get; private set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public LookupDto RequestStatus { get; private set; }
        public long? Id { get; set; }
    }
}
