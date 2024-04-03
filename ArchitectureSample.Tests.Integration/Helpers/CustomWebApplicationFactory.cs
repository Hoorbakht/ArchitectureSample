using ArchitectureSample.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureSample.Tests.Integration.Helpers;

public class CustomWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
{
	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.UseEnvironment("Testing");
		builder.ConfigureServices(services =>
		{
			var descriptor = services.SingleOrDefault(
				d => d.ServiceType ==
				     typeof(DbContextOptions<ArchitectureSampleContext>));

			if (descriptor != null)
				services.Remove(descriptor);

			services.AddDbContext<ArchitectureSampleContext>(options =>
				options.UseInMemoryDatabase("ArchitectureSampleDatabase"));
		}).UseEnvironment("Testing");
	}
}