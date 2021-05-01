using Domain.Interfaces;

namespace Domain.DomainModels
{
    public class UserApplication : EntityBase, IAggregateRoot
    {
        public virtual Application Application { get; set; }
        public virtual long ApplicationId { get; set; }
        public virtual User User { get; set; }
        public virtual string UserId { get; set; }
    }
}
