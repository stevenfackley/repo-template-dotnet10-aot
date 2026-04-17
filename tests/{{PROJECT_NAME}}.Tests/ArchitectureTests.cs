using System.Reflection;
using NetArchTest.Rules;
using Xunit;

namespace {{PROJECT_NAME}}.Tests;

public class ArchitectureTests
{
    private static readonly Assembly App = typeof(Program).Assembly;

    [Fact]
    public void No_banned_telemetry_dependencies()
    {
        var banned = new[]
        {
            "ApplicationInsights", "Sentry", "Segment", "Mixpanel",
            "Datadog", "NewRelic", "Raygun", "Rollbar", "Bugsnag",
        };

        foreach (var dep in App.GetReferencedAssemblies())
        {
            Assert.DoesNotContain(banned, b => dep.Name!.Contains(b, StringComparison.OrdinalIgnoreCase));
        }
    }

    [Fact]
    public void No_types_in_root_namespace()
    {
        var result = Types.InAssembly(App)
            .That().ResideInNamespace("{{PROJECT_NAME}}")
            .And().AreClasses()
            .And().DoNotHaveName("Program")
            .GetResult();

        Assert.True(result.IsSuccessful || result.LoadedTypes.Count == 0,
            "Non-Program classes should live in a sub-namespace.");
    }
}
