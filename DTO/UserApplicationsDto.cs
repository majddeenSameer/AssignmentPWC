using System.Collections.Generic;

namespace DTO
{
    public class UserApplicationsDto
    {
        public string UserName { get; set; }
        public List<long> ApplicationIds { get; set; }

    }
}
