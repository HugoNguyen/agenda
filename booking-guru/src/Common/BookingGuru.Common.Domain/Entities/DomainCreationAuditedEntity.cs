using BookingGuru.Common.Domain.Entities.Auditing;

namespace BookingGuru.Common.Domain.Entities;

public abstract class DomainCreationAuditedEntity<TPrimaryKey> : DomainEntity<TPrimaryKey>, ICreationAudited
{
    protected DomainCreationAuditedEntity()
    {
    }

    protected DomainCreationAuditedEntity(TPrimaryKey id) : base(id) { }

    public virtual DateTimeOffset CreationTime { get; set; }

    /// <summary>
    /// Creator of this entity.
    /// </summary>
    public virtual Guid? CreatorUserId { get; set; }
}

public abstract class DomainCreationAuditedEntity<TPrimaryKey, TUser> : DomainCreationAuditedEntity<TPrimaryKey>
    where TUser : IEntity<Guid>
{
    protected DomainCreationAuditedEntity()
    {
    }

    protected DomainCreationAuditedEntity(TPrimaryKey id) : base(id) { }

    public virtual TUser? CreatorUser { get; set; }
}