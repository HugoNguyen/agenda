namespace BookingGuru.Common.Domain.Entities.Auditing;

/// <summary>
/// This class can be used to simplify implementing <see cref="IAudited"/>.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
public abstract class AuditedEntity<TPrimaryKey, TUserPrimaryKey> : CreationAuditedEntity<TPrimaryKey, TUserPrimaryKey>, IAudited<TUserPrimaryKey>
{
    protected AuditedEntity() { }

    protected AuditedEntity(TPrimaryKey id) : base(id) { }

    /// <summary>
    /// Last modification date of this entity.
    /// </summary>
    public virtual DateTimeOffset? LastModificationTime { get; set; }

    /// <summary>
    /// Last modifier user of this entity.
    /// </summary>
    public virtual TUserPrimaryKey? LastModifierUserId { get; set; }
}

/// <summary>
/// This class can be used to simplify implementing <see cref="IAudited{TUser}"/>.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
/// <typeparam name="TUser">Type of the user</typeparam>
public abstract class AuditedEntity<TPrimaryKey, TUser, TUserPrimaryKey> : CreationAuditedEntity<TPrimaryKey, TUser, TUserPrimaryKey>, IAudited<TUserPrimaryKey>
    where TUser : IEntity<TUserPrimaryKey>
{
    protected AuditedEntity() { }

    protected AuditedEntity(TPrimaryKey id) : base(id) { }

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
