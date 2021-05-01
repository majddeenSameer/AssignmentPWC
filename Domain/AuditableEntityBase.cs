using System;
using System.Collections.Generic;
using System.Text;
using Domain.Interfaces;

namespace Domain
{
    public abstract class AuditableEntityBase : EntityBase, ISoftDeletable, IAuditable
    {
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
