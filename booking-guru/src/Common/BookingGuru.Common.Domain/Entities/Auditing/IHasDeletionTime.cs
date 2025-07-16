namespace BookingGuru.Common.Domain.Entities.Auditing;

//
// Summary:
//     An entity can implement this interface if Abp.Domain.Entities.Auditing.IHasDeletionTime.DeletionTime
//     of this entity must be stored. Domain.Entities.Auditing.IHasDeletionTime.DeletionTime
//     is automatically set when deleting Abp.Domain.Entities.Entity.
public interface IHasDeletionTime : ISoftDelete
{
    //
    // Summary:
    //     Deletion time of this entity.
    DateTimeOffset? DeletionTime { get; set; }
}