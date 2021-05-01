using DTO;

namespace Filters
{
    public class UsersFilterDto
    {
        public string UserName { get; set; }
        public string FirstNameAr { get; set; }
        public string FirstNameEn { get; set; }
        public ApplicationDto Application { get; set; }
        public ApplicationRoleDto Role { get; set; }
    }
}
