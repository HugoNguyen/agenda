namespace BookingGuru.Common.Domain.Entities.Auditing;

//
// Summary:
//     This interface ads Domain.Entities.Auditing.IDeletionAudited to Domain.Entities.Auditing.IAudited
//     for a fully audited entity.
public interface IFullAudited<TUserPrimaryKey> : IAudited<TUserPrimaryKey>, IDeletionAudited<TUserPrimaryKey>
    where TUserPrimaryKey : struct
{
}