using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Lookups
{
    public class UserStatus : LookupBase
    {
       public UserStatus()
        {

        }
        public static UserStatus Active = new UserStatus(1);
        public static UserStatus InActive = new UserStatus(2);
        public static UserStatus WaitingForAudit = new UserStatus(3);
        public static UserStatus Rejected = new UserStatus(4);

        public UserStatus(long id) : base(id)
        {
        }
    }
}
