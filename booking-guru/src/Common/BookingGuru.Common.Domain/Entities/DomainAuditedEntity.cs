using BookingGuru.Common.Domain.Entities.Auditing;

namespace BookingGuru.Common.Domain.Entities;

public abstract class DomainAuditedEntity<TPrimaryKey, TUserPrimaryKey> : DomainCreationAuditedEntity<TPrimaryKey, TUserPrimaryKey>, IAudited<TUserPrimaryKey>
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
    public virtual TUserPrimaryKey? LastModifierUserId { get; set; }
}

public abstract class DomainAuditedEntity<TPrimaryKey, TUser, TUserPrimaryKey> : DomainCreationAuditedEntity<TPrimaryKey, TUser, TUserPrimaryKey>, IAudited<TUserPrimaryKey>
    where TUser : IEntity<TUserPrimaryKey>
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
    public virtual TUserPrimaryKey? LastModifierUserId { get; set; }

    public virtual TUser? LastModifierUser { get; set; }
}
