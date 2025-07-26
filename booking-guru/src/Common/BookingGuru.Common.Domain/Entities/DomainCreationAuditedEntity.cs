using BookingGuru.Common.Domain.Entities.Auditing;

namespace BookingGuru.Common.Domain.Entities;

public abstract class DomainCreationAuditedEntity<TPrimaryKey, TUserPrimaryKey> : DomainEntity<TPrimaryKey>, ICreationAudited<TUserPrimaryKey>
{
    protected DomainCreationAuditedEntity()
    {
    }

    protected DomainCreationAuditedEntity(TPrimaryKey id) : base(id) { }

    public virtual DateTimeOffset CreationTime { get; set; }

    /// <summary>
    /// Creator of this entity.
    /// </summary>
    public virtual TUserPrimaryKey? CreatorUserId { get; set; }
}

public abstract class DomainCreationAuditedEntity<TPrimaryKey, TUser, TUserPrimaryKey> : DomainCreationAuditedEntity<TPrimaryKey, TUserPrimaryKey>
    where TUser : IEntity<TUserPrimaryKey>
{
    protected DomainCreationAuditedEntity()
    {
    }

    protected DomainCreationAuditedEntity(TPrimaryKey id) : base(id) { }

    public virtual TUser? CreatorUser { get; set; }
}