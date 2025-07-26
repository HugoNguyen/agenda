namespace BookingGuru.Common.Domain.Entities.Auditing;

//
// Summary:
//     This interface is implemented by entities that is wanted to store creation information
//     (who and when created). Creation time and creator user are automatically set
//     when saving Domain.Entities.Entity to database.
public interface ICreationAudited<TUserPrimaryKey> : IHasCreationTime
{
    //
    // Summary:
    //     Id of the creator user of this entity.
    TUserPrimaryKey? CreatorUserId { get; set; }
}

//
// Summary:
//     Adds navigation properties to Domain.Entities.Auditing.ICreationAudited interface
//     for user.
//
// Type parameters:
//   TUser:
//     Type of the user
public interface ICreationAudited<TUser, TUserPrimaryKey> : ICreationAudited<TUserPrimaryKey> where TUser : IEntity<TUserPrimaryKey>
{
    //
    // Summary:
    //     Reference to the creator user of this entity.
    TUser CreatorUser { get; set; }
}