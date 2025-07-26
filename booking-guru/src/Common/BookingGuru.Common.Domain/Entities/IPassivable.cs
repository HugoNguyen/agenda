namespace BookingGuru.Common.Domain.Entities;

//
// Summary:
//     This interface is used to make an entity active/passive.
public interface IPassivable
{
    //
    // Summary:
    //     True: This entity is active. False: This entity is not active.
    bool IsActive { get; set; }
}