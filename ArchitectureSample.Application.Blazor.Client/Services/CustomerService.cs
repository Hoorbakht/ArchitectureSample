using System.Net.Http.Json;
using System.Text.Json;
using ArchitectureSample.Application.Blazor.Client.Dtos;

namespace ArchitectureSample.Application.Blazor.Client.Services;

public class CustomerService(HttpClient httpClient) : ICustomerService
{
	public async Task<QueryApiResponse<CustomerDto>?> Get(QueryApiRequest apiRequest)
	{
		var request = new HttpRequestMessage(HttpMethod.Get, "api/Customer");
		
		request.Headers.Add("x-query", JsonSerializer.Serialize(apiRequest));
		
		var response = await httpClient.SendAsync(request);

		if (response.IsSuccessStatusCode)
		{
			return JsonSerializer.Deserialize<QueryApiResponse<CustomerDto>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});
		}
		return null;
	}

	public Task<CustomerDto> Get(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task Delete(Guid id)
	{
		throw new NotImplementedException();
	}

	public async Task<HttpResponseMessage> Create(CustomerDto customerDto)
	{
		var response = await httpClient.PostAsJsonAsync("/api/customer", new CreateCustomerModel
		(
			customerDto.FirstName,
			customerDto.LastName,
			customerDto.DateOfBirth,
			customerDto.PhoneNumber,
			customerDto.Email,
			customerDto.BankAccount
		));

		return response;
	}

	public Task Update(CustomerDto customerDto)
	{
		throw new NotImplementedException();
	}
}