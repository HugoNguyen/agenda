namespace BookingGuru.Common.Domain.Entities.Auditing;

//
// Summary:
//     An entity can implement this interface if Domain.Entities.Auditing.IHasCreationTime.CreationTime
//     of this entity must be stored. Domain.Entities.Auditing.IHasCreationTime.CreationTime
//     is automatically set when saving Domain.Entities.Entity to database.
public interface IHasCreationTime
{
    //
    // Summary:
    //     Creation time of this entity.
    DateTimeOffset CreationTime { get; set; }
}