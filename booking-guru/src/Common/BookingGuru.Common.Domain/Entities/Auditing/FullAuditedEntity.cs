﻿namespace BookingGuru.Common.Domain.Entities.Auditing;

public abstract class FullAuditedEntity<TPrimaryKey, TUserPrimaryKey> : AuditedEntity<TPrimaryKey, TUserPrimaryKey>, IFullAudited<TUserPrimaryKey>
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
    public virtual TUserPrimaryKey? DeleterUserId { get; set; }

    /// <summary>
    /// Deletion time of this entity.
    /// </summary>
    public virtual DateTimeOffset? DeletionTime { get; set; }
}

public abstract class FullAuditedEntity<TPrimaryKey, TUser, TUserPrimaryKey> : AuditedEntity<TPrimaryKey, TUser, TUserPrimaryKey>, IFullAudited<TUserPrimaryKey>
    where TUser : IEntity<TUserPrimaryKey>
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
    public virtual TUserPrimaryKey? DeleterUserId { get; set; }

    /// <summary>
    /// Deletion time of this entity.
    /// </summary>
    public virtual DateTimeOffset? DeletionTime { get; set; }

    public virtual TUser? DeleterUser { get; set; }
}