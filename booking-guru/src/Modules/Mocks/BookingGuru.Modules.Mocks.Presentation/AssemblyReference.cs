using System.Reflection;

namespace BookingGuru.Modules.Mocks.Presentation;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}