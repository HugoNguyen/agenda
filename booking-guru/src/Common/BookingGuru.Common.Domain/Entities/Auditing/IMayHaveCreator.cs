namespace BookingGuru.Common.Domain.Entities.Auditing;

public interface IMayHaveCreator
{
    //
    // Summary:
    //     Id of the creator user of this entity.
    Guid? CreatorUserId { get; set; }
}

public interface IMayHaveCreator<TUser>
{
    //
    // Summary:
    //     Reference to the creator user of this entity.
    TUser CreatorUser { get; set; }
}