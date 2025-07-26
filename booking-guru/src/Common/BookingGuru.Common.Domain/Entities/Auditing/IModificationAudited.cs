namespace BookingGuru.Common.Domain.Entities.Auditing;

//
// Summary:
//     This interface is implemented by entities that is wanted to store modification
//     information (who and when modified lastly). Properties are automatically set
//     when updating the Domain.Entities.IEntity.
public interface IModificationAudited<TUserPrimaryKey> : IHasModificationTime where TUserPrimaryKey : struct
{
    //
    // Summary:
    //     Last modifier user for this entity.
    TUserPrimaryKey? LastModifierUserId { get; set; }
}

//
// Summary:
//     Adds navigation properties to Domain.Entities.Auditing.IModificationAudited interface
//     for user.
//
// Type parameters:
//   TUser:
//     Type of the user
public interface IModificationAudited<TUser, TUserPrimaryKey> : IModificationAudited<TUserPrimaryKey> where TUser : IEntity<TUserPrimaryKey> where TUserPrimaryKey : struct
{
    //
    // Summary:
    //     Reference to the creator user of this entity.
    TUser LastModifierUser { get; set; }
}