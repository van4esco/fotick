using System;

namespace DAL.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime AddedDate { get; set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            AddedDate = DateTime.UtcNow;
        }
    }
}
