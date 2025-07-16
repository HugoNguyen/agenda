
namespace BookingGuru.Common.Domain.Entities.Auditing;

//
// Summary:
//     An entity can implement this interface if Domain.Entities.Auditing.IHasModificationTime.LastModificationTime
//     of this entity must be stored. Domain.Entities.Auditing.IHasModificationTime.LastModificationTime
//     is automatically set when updating Domain.Entities.Entity.
public interface IHasModificationTime
{
    //
    // Summary:
    //     The last modified time for this entity.
    DateTimeOffset? LastModificationTime { get; set; }
}