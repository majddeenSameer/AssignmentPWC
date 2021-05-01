using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Domain
{
    public class AuthorizationDetails
    {

        public AuthorizationDetails(IEnumerable<Claim> claims)
        {
            this.Claims = claims;
        }

        public AuthorizationDetails()
        {

        }

        public IEnumerable<Claim> Claims { get; set; }

        public string Username { get { return Claims.SingleOrDefault(x => x.Type == "UserName")?.Value; } }
        public string UserId { get { return Claims.SingleOrDefault(x => x.Type == "UserId")?.Value; } }
        public string NameAr { get { return Claims.SingleOrDefault(x => x.Type == "NameAr")?.Value; } }
        public string NameEn { get { return Claims.SingleOrDefault(x => x.Type == "NameEn")?.Value; } }
        public List<string> Roles { get { return Claims.Where(x => x.Type == ClaimTypes.Role)?.Select(x => x.Value).ToList(); } }
    }
}
