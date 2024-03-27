using ArchitectureSample.Domain.Entities;
using ArchitectureSample.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureSample.Tests.Integration.Helpers;

public class CustomWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
{
	protected override void ConfigureWebHost(IWebHostBuilder builder) =>
	    builder.ConfigureServices(services =>
	    {
		    var descriptor = services.SingleOrDefault(
		    d => d.ServiceType ==
		     typeof(DbContextOptions<ArchitectureSampleContext>));

		    if (descriptor != null)
			    services.Remove(descriptor);

		    services.AddDbContext<ArchitectureSampleContext>(options => options.UseInMemoryDatabase("ArchitectureSampleDatabase"));

		    using var scope = services.BuildServiceProvider();

		    using var context = scope.GetRequiredService<ArchitectureSampleContext>();

		    context.Customers.Add(Customer.Create(
			    Constants.SampleId,
			    Constants.ValidSampleFirstName,
			    Constants.ValidSampleLastName,
			    Constants.ValidSampleBirthOfDate,
			    Constants.ValidSamplePhoneNumber,
			    Constants.ValidSampleEmail,
			    Constants.ValidSampleBankAccount));
		    context.SaveChanges();

	    }).UseEnvironment("Testing");
}