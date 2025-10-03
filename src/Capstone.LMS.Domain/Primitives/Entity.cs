using System;

namespace Capstone.LMS.Domain.Primitives
{
    public abstract class Entity : IEquatable<Entity>, IAudit, IPublicId
    {
        protected Entity() 
        { 

        }

        protected Entity(Guid id) 
        {
            Id = id;
        }

        public Guid Id { get; protected set; }
        public Guid PublicId { get; private set; }
        public Guid CreatedBy { get; private set; }
        public Guid ModifiedBy { get; private set; }
        public Guid? DeletedBy { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime ModifiedOnUtc { get; private set; }
        public DateTime? DeletedOnUtc { get; private set; }

        public void Created(Guid createdBy)
        {
            CreatedBy = createdBy;
            ModifiedBy = createdBy;
            CreatedOnUtc = DateTime.UtcNow;
            ModifiedOnUtc = DateTime.UtcNow;
        }

        public void Modified(Guid modifiedBy)
        {
            ModifiedBy = modifiedBy;
            ModifiedOnUtc = DateTime.UtcNow;
        }

        public void Deleted(Guid deletedBy)
        {
            DeletedBy = deletedBy;
            DeletedOnUtc = DateTime.UtcNow;
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

        public void SetPublicId() =>
            PublicId = Guid.NewGuid();
    }
}
