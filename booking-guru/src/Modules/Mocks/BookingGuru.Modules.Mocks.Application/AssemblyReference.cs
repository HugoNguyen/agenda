using System.Reflection;

namespace BookingGuru.Modules.Mocks.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}