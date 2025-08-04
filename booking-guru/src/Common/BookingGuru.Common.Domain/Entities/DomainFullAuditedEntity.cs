using BookingGuru.Common.Domain.Entities.Auditing;

namespace BookingGuru.Common.Domain.Entities;

public abstract class DomainFullAuditedEntity<TPrimaryKey> : DomainAuditedEntity<TPrimaryKey>, IFullAudited
{
    protected DomainFullAuditedEntity() { }

    protected DomainFullAuditedEntity(TPrimaryKey id) : base(id) { }

    /// <summary>
    /// Is this entity Deleted?
    /// </summary>
    public virtual bool IsDeleted { get; set; }

    /// <summary>
    /// Which user deleted this entity?
    /// </summary>
    public virtual Guid? DeleterUserId { get; set; }

    /// <summary>
    /// Deletion time of this entity.
    /// </summary>
    public virtual DateTimeOffset? DeletionTime { get; set; }
}

public abstract class DomainFullAuditedEntity<TPrimaryKey, TUser> : DomainAuditedEntity<TPrimaryKey, TUser>, IFullAudited
    where TUser : IEntity<Guid>
{
    protected DomainFullAuditedEntity() { }

    protected DomainFullAuditedEntity(TPrimaryKey id) : base(id) { }

    /// <summary>
    /// Is this entity Deleted?
    /// </summary>
    public virtual bool IsDeleted { get; set; }

    /// <summary>
    /// Which user deleted this entity?
    /// </summary>
    public virtual Guid? DeleterUserId { get; set; }

    /// <summary>
    /// Deletion time of this entity.
    /// </summary>
    public virtual DateTimeOffset? DeletionTime { get; set; }

    public virtual TUser? DeleterUser { get; set; }
}