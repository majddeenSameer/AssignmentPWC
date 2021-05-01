using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using Domain.Interfaces;
using Domain.Lookups;

namespace Domain.DomainModels
{
    public class Complaint : AuditableEntityBase, IAggregateRoot
    {
        public virtual NoAuthUser NoAuthUser { get; private set; }
        public virtual long?  NoAuthUserID { get; set; }
        public virtual string Title { get; set; }
        public virtual string Details { get; set; }
        public virtual RequestStatus RequestStatus { get; private set; }
        public virtual long? RequestStatusId { get; set; }
    }
}
