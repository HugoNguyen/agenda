namespace BookingGuru.Common.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class ApplyMigrationAttribute : Attribute
{
}