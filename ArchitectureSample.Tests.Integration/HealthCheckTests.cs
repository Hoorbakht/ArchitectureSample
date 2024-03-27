using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ArchitectureSample.Tests.Integration;

public class HealthCheckTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
	[Fact]
	public async Task HealthCheck_ReturnOk()
	{
		var response = await factory.CreateDefaultClient().GetAsync("/HealthChecks");

		response.StatusCode.Should().Be(HttpStatusCode.OK);
		response.Content.Headers.ContentLength.Should().BeGreaterThan(0);
		var result = await response.Content.ReadAsStringAsync();
		result.Should().NotBeNullOrWhiteSpace();
		result.Should().Be("Healthy");
	}
}