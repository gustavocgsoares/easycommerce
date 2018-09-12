using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Easy.Commerce.Domain.Shared
{
    public abstract class Entity<TEntity, TId> : BaseEntity
        where TEntity : Entity<TEntity, TId>
    {
        public virtual TId Id { get; set; }

        [NotMapped]
        public virtual int? Version { get; set; }

        [NotMapped]
        public virtual List<string> Advices { get; set; }

        [NotMapped]
        protected virtual int? RequestedHashCode { get; set; }

        public static bool operator ==(Entity<TEntity, TId> left, Entity<TEntity, TId> right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Entity<TEntity, TId> left, Entity<TEntity, TId> right)
        {
            return !(left == right);
        }

        public bool IsTransient()
        {
            return EqualityComparer<TId>.Default.Equals(Id, default(TId));
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TEntity, TId>))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var item = (Entity<TEntity, TId>)obj;

            if (item.IsTransient() || IsTransient())
            {
                return false;
            }

            return EqualityComparer<TId>.Default.Equals(item.Id, Id);
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!RequestedHashCode.HasValue)
                {
                    RequestedHashCode = Id.GetHashCode() ^ 31;
                }

                return RequestedHashCode.Value;
            }

            return base.GetHashCode();
        }

        public virtual T Clone<T>()
        {
            return (T)Clone();
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        public void AddValidation(string message)
        {
            if (Advices == null)
            {
                Advices = new List<string>();
            }

            Advices.Add(message);
        }
    }
}