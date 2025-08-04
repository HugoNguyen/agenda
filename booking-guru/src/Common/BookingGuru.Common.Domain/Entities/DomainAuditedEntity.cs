using BookingGuru.Common.Domain.Entities.Auditing;

namespace BookingGuru.Common.Domain.Entities;

public abstract class DomainAuditedEntity<TPrimaryKey> : DomainCreationAuditedEntity<TPrimaryKey>, IAudited
{
    protected DomainAuditedEntity() { }

    protected DomainAuditedEntity(TPrimaryKey id) : base(id) { }

    /// <summary>
    /// Last modification date of this entity.
    /// </summary>
    public virtual DateTimeOffset? LastModificationTime { get; set; }

    /// <summary>
    /// Last modifier user of this entity.
    /// </summary>
    public virtual Guid? LastModifierUserId { get; set; }
}

public abstract class DomainAuditedEntity<TPrimaryKey, TUser> : DomainCreationAuditedEntity<TPrimaryKey, TUser>, IAudited
    where TUser : IEntity<Guid>
{
    protected DomainAuditedEntity() { }

    protected DomainAuditedEntity(TPrimaryKey id) : base(id) { }

    /// <summary>
    /// Last modification date of this entity.
    /// </summary>
    public virtual DateTimeOffset? LastModificationTime { get; set; }

    /// <summary>
    /// Last modifier user of this entity.
    /// </summary>
    public virtual Guid? LastModifierUserId { get; set; }

    public virtual TUser? LastModifierUser { get; set; }
}
