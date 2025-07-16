namespace BookingGuru.Common.Domain.Entities;

//
// Summary:
//     Used to standardize soft deleting entities. Soft-delete entities are not actually
//     deleted, marked as IsDeleted = true in the database, but can not be retrieved
//     to the application.
public interface ISoftDelete
{
    //
    // Summary:
    //     Used to mark an Entity as 'Deleted'.
    bool IsDeleted { get; set; }
}