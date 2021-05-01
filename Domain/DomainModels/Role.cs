using Microsoft.AspNetCore.Identity;

namespace Domain.DomainModels
{
    public class Role : IdentityRole
    {
        public Role() : base() { }
        public Role(string username, long applicationId) : base(username)
        {
            ApplicationId = applicationId;
        }

        public virtual Application Application { get; set; }
        public virtual long ApplicationId { get; set; }
    }
}
