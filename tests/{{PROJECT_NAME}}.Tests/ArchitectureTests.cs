using System.Reflection;
using NetArchTest.Rules;
using Xunit;

namespace {{PROJECT_NAME}}.Tests;

public class ArchitectureTests
{
    private const string RootNamespace = "{{PROJECT_NAME}}";
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
        // ResideInNamespace matches sub-namespaces too, so filter to an exact
        // match: the rule is that only Program sits directly in the root.
        var offenders = Types.InAssembly(App)
            .That().ResideInNamespace(RootNamespace)
            .And().AreClasses()
            .And().DoNotHaveName("Program")
            .GetTypes()
            .Where(t => t.Namespace == RootNamespace)
            .Select(t => t.FullName)
            .ToList();

        Assert.True(offenders.Count == 0,
            $"Non-Program classes should live in a sub-namespace: {string.Join(", ", offenders)}");
    }
}
