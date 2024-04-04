using ArchitectureSample.Application.Api;
using ArchitectureSample.Application.Dtos;
using ArchitectureSample.Domain.Entities;
using ArchitectureSample.Infrastructure.Data;
using ArchitectureSample.Infrastructure.Logging.Dtos;
using ArchitectureSample.Infrastructure.Logging.Helpers;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Config;
using NLog.Web;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.SetMinimumLevel(LogLevel.Trace).AddNLogWeb();

builder.Host.UseNLog();

LogManager.Configuration = new LoggingConfiguration()
	.AddErrorJsonFileConfiguration()
	.AddConsoleConfiguration()
	.AddMongoConfiguration(
		MongoOptions.CreateApplicationOptions(builder.Configuration.GetConnectionString("Mongo")!,
			"ArchitectureSample"));

var logger = LogManager.Setup().GetLogger("Application");

try
{
	builder.Services.AddCoreServices(builder.Configuration, typeof(Anchor), builder.Environment);

	var app = builder.Build();
	app.UseCoreApplication(builder.Environment);

	await using var scope = app.Services.CreateAsyncScope();

	await using var context = scope.ServiceProvider.GetRequiredService<ArchitectureSampleContext>();

	await context.Database.EnsureCreatedAsync();

	if (!await context.Customers.AnyAsync())
	{
		await context.Customers.AddRangeAsync(new List<Customer>
		{
			Customer.Create(Guid.NewGuid(), "Mahyar", "Hoorbakht", DateTime.Now.AddYears(-32),
				"+989365386860", "Mahyar.hoorbakht@outlook.com", "1234567890"),
			Customer.Create(Guid.NewGuid(), "Mason", "Chase", DateTime.Now.AddYears(-40), "+31633300635",
				"Mason.chase@outlook.com", "0987654321"),
			Customer.Create(Guid.NewGuid(), "Mohammad", "Afshar", DateTime.Now.AddYears(-35),
				"+31616833033", "Mohammad.afshar@outlook.com", "6789012345"),
			Customer.Create(Guid.NewGuid(), "Elnaz", "Makhtoom", DateTime.Now.AddYears(-32),
				"+989115185125", "Elnaz.makhtoom@outlook.com", "5678901234"),
			Customer.Create(Guid.NewGuid(), "Sample Name", "Sample Family", DateTime.Now.AddYears(-20),
				"+989209837654", "sample.sample@outlook.com", "6781234590"),
			Customer.Create(Guid.NewGuid(), "John", "Doe", DateTime.Now.AddYears(-60), "+989129906543",
				"John.doe@outlook.com", "8912345670")
		});

		await context.SaveChangesAsync();
	}

	await app.RunAsync();
}
catch (Exception exception)
{
	logger.CustomError(new MongoLog
	{
		LoggerName = "Program",
		Message = "Program Stopped Because of Exception !",
		CustomData = new MongoLogCustomData
		{
			ClassName = nameof(Program),
			MethodName = "Main",
			Exception = exception
		}
	});
	throw;
}
finally
{
	NLog.LogManager.Shutdown();
}

public partial class Program
{
}