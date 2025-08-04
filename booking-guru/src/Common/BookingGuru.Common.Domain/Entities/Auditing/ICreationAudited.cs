namespace BookingGuru.Common.Domain.Entities.Auditing;

//
// Summary:
//     This interface is implemented by entities that is wanted to store creation information
//     (who and when created). Creation time and creator user are automatically set
//     when saving Domain.Entities.Entity to database.
public interface ICreationAudited : IHasCreationTime, IMayHaveCreator
{
}

//
// Summary:
//     Adds navigation properties to Domain.Entities.Auditing.ICreationAudited interface
//     for user.
//
// Type parameters:
//   TUser:
//     Type of the user
public interface ICreationAudited<TUser> : ICreationAudited, IMayHaveCreator<TUser> where TUser : IEntity<Guid>
{
}