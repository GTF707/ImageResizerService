using System;

namespace Domain
{
    public interface IDeletableObject
    {
        bool IsDeleted { get; set; }
        DateTime DeleteDate { get; set; }
    }
}
