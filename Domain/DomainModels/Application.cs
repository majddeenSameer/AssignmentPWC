using System.Collections.Generic;
using Domain.Interfaces;

namespace Domain.DomainModels
{
    public class Application : EntityBase, IAggregateRoot
    {
        public virtual string Code { get; set; }
        public virtual string NameAr { get; set; }
        public virtual string NameEn { get; set; }
        public virtual IList<Role> Roles { get; set; }
    }
}
