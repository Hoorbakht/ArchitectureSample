using ArchitectureSample.Application.Api;
using ArchitectureSample.Application.Dtos;
using ArchitectureSample.Domain.Entities;
using ArchitectureSample.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCoreServices(builder.Configuration, typeof(Anchor), builder.Environment);

var app = builder.Build();
app.UseCoreApplication(builder.Environment);

if (!builder.Environment.IsProduction())
{
	await using var scope = app.Services.CreateAsyncScope();

	await using var context = scope.ServiceProvider.GetRequiredService<ArchitectureSampleContext>();

	await context.Database.EnsureDeletedAsync();
	await context.Database.EnsureCreatedAsync();

	await context.Customers.AddRangeAsync(new List<Customer>
	{
		Customer.Create(Guid.NewGuid(), "Mahyar","Hoorbakht",DateTime.Now.AddYears(-30),"+989365386860","Mahyar.hoorbakht@outlook.com","1234567890"),
		Customer.Create(Guid.NewGuid(), "Mahyar 2","Hoorbakht 2",DateTime.Now.AddYears(-30),"+989365386860","Mahyar.hoorbakht@outlook.com","1234567890"),
		Customer.Create(Guid.NewGuid(), "Mahyar 3","Hoorbakht 3",DateTime.Now.AddYears(-30),"+989365386860","Mahyar.hoorbakht@outlook.com","1234567890"),
		Customer.Create(Guid.NewGuid(), "Mahyar 4","Hoorbakht 4",DateTime.Now.AddYears(-30),"+989365386860","Mahyar.hoorbakht@outlook.com","1234567890"),
		Customer.Create(Guid.NewGuid(), "Mahyar 5","Hoorbakht 5",DateTime.Now.AddYears(-30),"+989365386860","Mahyar.hoorbakht@outlook.com","1234567890"),
		Customer.Create(Guid.NewGuid(), "Mahyar 6","Hoorbakht 6",DateTime.Now.AddYears(-30),"+989365386860","Mahyar.hoorbakht@outlook.com","1234567890")
	});

	await context.SaveChangesAsync();
}
await app.RunAsync();

public partial class Program
{
}