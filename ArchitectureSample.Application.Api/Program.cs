using ArchitectureSample.Application.Api;
using ArchitectureSample.Application.Dtos;
using ArchitectureSample.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost
//	.UseUrls("http://localhost:5000", "https://localhost:5001", "https://192.168.0.104:5001");

builder.Services.AddCoreServices(builder.Configuration, typeof(Anchor));

var app = builder.Build();
app.UseCoreApplication(builder.Environment);

await using var scope = app.Services.CreateAsyncScope();

await using var context = scope.ServiceProvider.GetRequiredService<ArchitectureSampleContext>();

await context.Database.EnsureCreatedAsync();

await app.RunAsync();

public partial class Program
{
}