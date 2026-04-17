using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddHealthChecks();

var app = builder.Build();
app.MapHealthChecks("/health");
app.MapGet("/", () => Results.Ok(new { service = "{{PROJECT_NAME}}", status = "ok" }));

app.Run();

public partial class Program;
