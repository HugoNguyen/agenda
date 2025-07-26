using System.Reflection;

namespace BookingGuru.Modules.Mock2s.Infrastructure;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}