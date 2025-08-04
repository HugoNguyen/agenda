namespace BookingGuru.Common.Domain.Entities.Auditing;

/// <summary>
/// This class can be used to simplify implementing <see cref="IAudited"/>.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
public abstract class AuditedEntity<TPrimaryKey> : CreationAuditedEntity<TPrimaryKey>, IAudited
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
    public virtual Guid? LastModifierUserId { get; set; }
}

/// <summary>
/// This class can be used to simplify implementing <see cref="IAudited{TUser}"/>.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
/// <typeparam name="TUser">Type of the user</typeparam>
public abstract class AuditedEntity<TPrimaryKey, TUser> : CreationAuditedEntity<TPrimaryKey, TUser>, IAudited
    where TUser : IEntity<Guid>
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
    public virtual Guid? LastModifierUserId { get; set; }

    public virtual TUser? LastModifierUser { get; set; }
}
