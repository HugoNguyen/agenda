using System.Reflection;

namespace BookingGuru.Modules.Mock2s.ArchitectureTests.Abstractions;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(Mock2s.Application.AssemblyReference).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(Mock2s.Domain.AssemblyReference).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(Infrastructure.AssemblyReference).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(Mock2s.Presentation.AssemblyReference).Assembly;
}
