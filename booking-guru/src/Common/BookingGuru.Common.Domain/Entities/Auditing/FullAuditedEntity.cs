namespace BookingGuru.Common.Domain.Entities.Auditing;

public abstract class FullAuditedEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IFullAudited
{
    protected FullAuditedEntity() { }

    protected FullAuditedEntity(TPrimaryKey id) : base(id) { }

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

public abstract class FullAuditedEntity<TPrimaryKey, TUser> : AuditedEntity<TPrimaryKey, TUser>, IFullAudited
    where TUser : IEntity<Guid>
{
    protected FullAuditedEntity() { }

    protected FullAuditedEntity(TPrimaryKey id) : base(id) { }

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