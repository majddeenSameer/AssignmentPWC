using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IIdentityService
    {
        AuthorizationDetails GetAuthorizationDetails();
    }
}
