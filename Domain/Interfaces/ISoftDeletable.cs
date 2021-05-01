using System;

namespace Domain.Interfaces
{
    public interface ISoftDeletable
    {
        long Id { get; set; }
        string ModifiedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
        bool IsDeleted { get; set; }
    }
}