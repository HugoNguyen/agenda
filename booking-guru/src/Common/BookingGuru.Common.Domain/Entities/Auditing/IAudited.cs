namespace BookingGuru.Common.Domain.Entities.Auditing;

//
// Summary:
//     This interface is implemented by entities which must be audited. Related properties
//     automatically set when saving/updating Abp.Domain.Entities.Entity objects.
public interface IAudited : ICreationAudited, IModificationAudited
{
}
