using System.Text.Json;
using ArchitectureSample.Application.Blazor.Client.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectureSample.Application.Blazor.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
	private readonly HttpClient _httpClient;

	public CustomerController(IEnumerable<HttpClient> httpClients)
	{
		_httpClient = httpClients.Single(x => x.BaseAddress!.Port != 9090);
	}

	[HttpGet]
	public async Task<IActionResult> Get(string page)
	{
		var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/customers");

		request.Headers.Add("x-query", "{page : " + page + "}");

		var response = await _httpClient.SendAsync(request);

		return response.IsSuccessStatusCode
			? Ok(await response.Content.ReadAsStringAsync())
			: BadRequest(await response.Content.ReadAsStringAsync());
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> Get(Guid id)
	{
		return Ok(id);
	}

	[HttpPost]
	public async Task<IActionResult> Create(CreateCustomerModel customer)
	{
		var response = await _httpClient.PostAsJsonAsync("api/v1/customers", new
		{
			Model = new Create<CreateCustomerModel>
			{
				Model = customer
			}
		});

		if (response.IsSuccessStatusCode) return Ok();

		var content = await response.Content.ReadAsStringAsync();

		return BadRequest(content);
	}

	[HttpPut]
	public async Task<IActionResult> Update()
	{
		return Ok("");
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		return Ok(id);
	}
}