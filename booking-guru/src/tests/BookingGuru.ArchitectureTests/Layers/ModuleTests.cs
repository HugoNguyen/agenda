using System.Reflection;
using BookingGuru.ArchitectureTests.Abstractions;
using NetArchTest.Rules;

namespace BookingGuru.ArchitectureTests.Layers;

public class ModuleTests : BaseTest
{
    [Fact]
    public void MocksModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [Mock2sNamespace];
        string[] integrationEventsModules = [
            Mock2sIntegrationEventsNamespace];

        List<Assembly> mocksAssemblies =
        [
            Modules.Mocks.Domain.AssemblyReference.Assembly,
            Modules.Mocks.Application.AssemblyReference.Assembly,
            Modules.Mocks.Infrastructure.AssemblyReference.Assembly,
            Modules.Mocks.Presentation.AssemblyReference.Assembly,
        ];

        Types.InAssemblies(mocksAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void Mock2sModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [MocksNamespace];
        string[] integrationEventsModules = [
            MocksIntegrationEventsNamespace];

        List<Assembly> mock2sAssemblies =
        [
            Modules.Mock2s.Domain.AssemblyReference.Assembly,
            Modules.Mock2s.Application.AssemblyReference.Assembly,
            Modules.Mock2s.Infrastructure.AssemblyReference.Assembly,
            Modules.Mock2s.Presentation.AssemblyReference.Assembly,
        ];

        Types.InAssemblies(mock2sAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }
}
