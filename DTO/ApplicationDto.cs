using System.Collections.Generic;

namespace DTO
{
    public class ApplicationDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public IList<RoleDto> Roles { get; set; }
    }
}
