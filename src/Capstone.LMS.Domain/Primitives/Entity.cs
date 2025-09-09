using System;

namespace Capstone.LMS.Domain.Primitives
{
    public abstract class Entity : IEquatable<Entity>, IAudit
    {
        protected Entity(Guid id) 
        {
            Id = id;
        }

        public Guid Id { get; }

        public Guid CreatedBy { get; private set; }
        public Guid ModifiedBy { get; private set; }
        public Guid? DeletedBy { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime ModifiedAtUtc { get; private set; }
        public DateTime? DeletedAtUtc { get; private set; }

        public void Created()
        {
            CreatedAtUtc = DateTime.UtcNow;
            ModifiedAtUtc = DateTime.UtcNow;
        }

        public void Modified()
        {
            ModifiedAtUtc = DateTime.UtcNow;
        }

        public void Deleted()
        {
            DeletedAtUtc = DateTime.UtcNow;
        }

        public static bool operator ==(Entity first, Entity second)
        {
            return first is not null && second is not null && first.Equals(second);
        }

        public static bool operator !=(Entity first, Entity second)
        {
            return !(first == second);
        }

        public override bool Equals(object obj)
        {
            if(obj is null)
            {
                return false;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            if (obj is not Entity entity)
            {
                return false;
            }

            return entity.Id == Id;
        }

        public bool Equals(Entity other)
        {
            if (other is null)
            {
                return false;
            }

            if (other.GetType() != GetType())
            {
                return false;
            }

            return other.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
