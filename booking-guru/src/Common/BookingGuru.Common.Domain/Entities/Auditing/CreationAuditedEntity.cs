namespace BookingGuru.Common.Domain.Entities.Auditing;

/// <summary>
/// This class can be used to simplify implementing <see cref="ICreationAudited"/>.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
public abstract class CreationAuditedEntity<TPrimaryKey, TUserPrimaryKey> : Entity<TPrimaryKey>, ICreationAudited<TUserPrimaryKey>
{
    protected CreationAuditedEntity()
    {
    }

    protected CreationAuditedEntity(TPrimaryKey id) : base(id) { }

    public virtual DateTimeOffset CreationTime { get; set; }

    /// <summary>
    /// Creator of this entity.
    /// </summary>
    public virtual TUserPrimaryKey? CreatorUserId { get; set; }
}

/// <summary>
/// This class can be used to simplify implementing <see cref="ICreationAudited{TUser}"/>.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
/// <typeparam name="TUser">Type of the user</typeparam>
public abstract class CreationAuditedEntity<TPrimaryKey, TUser, TUserPrimaryKey> : CreationAuditedEntity<TPrimaryKey, TUserPrimaryKey>
    where TUser : IEntity<TUserPrimaryKey>
{
    protected CreationAuditedEntity()
    {
    }

    protected CreationAuditedEntity(TPrimaryKey id) : base(id) { }

    public virtual TUser? CreatorUser { get; set; }
}