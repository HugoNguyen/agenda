using System.Reflection;

namespace BookingGuru.Modules.Mocks.ArchitectureTests.Abstractions;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(Mocks.Application.AssemblyReference).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(Mocks.Domain.AssemblyReference).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(Infrastructure.AssemblyReference).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(Mocks.Presentation.AssemblyReference).Assembly;
}
