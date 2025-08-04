namespace BookingGuru.Common.Domain.Entities.Auditing;

//
// Summary:
//     This interface is implemented by entities which wanted to store deletion information
//     (who and when deleted).
public interface IDeletionAudited : IHasDeletionTime
{
    //
    // Summary:
    //     Which user deleted this entity?
    Guid? DeleterUserId { get; set; }
}