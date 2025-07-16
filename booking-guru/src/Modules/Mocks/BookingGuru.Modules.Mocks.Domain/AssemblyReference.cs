using System.Reflection;

namespace BookingGuru.Modules.Mocks.Domain;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}