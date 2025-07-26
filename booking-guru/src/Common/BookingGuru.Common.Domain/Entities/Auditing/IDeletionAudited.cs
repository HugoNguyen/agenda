namespace BookingGuru.Common.Domain.Entities.Auditing;

//
// Summary:
//     This interface is implemented by entities which wanted to store deletion information
//     (who and when deleted).
public interface IDeletionAudited<TUserPrimaryKey> : IHasDeletionTime
{
    //
    // Summary:
    //     Which user deleted this entity?
    TUserPrimaryKey? DeleterUserId { get; set; }
}