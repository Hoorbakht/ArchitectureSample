using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using ArchitectureSample.Application.Blazor.Client.Dtos;

namespace ArchitectureSample.Application.Blazor.Client.Services;

public class CustomerService(HttpClient httpClient) : ICustomerService
{
	public async Task<ApiResponse<Data<CustomerDto>>?> Get(QueryApiRequest apiRequest)
	{
		var request = new HttpRequestMessage(HttpMethod.Get, "api/Customer");

		request.Headers.Add("x-query", JsonSerializer.Serialize(apiRequest));

		var response = await httpClient.SendAsync(request);

		if (response.IsSuccessStatusCode)
		{
			return JsonSerializer.Deserialize<ApiResponse<Data<CustomerDto>>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
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

	public async Task<ApiResponse<CustomerDto>?> Delete(Guid id)
	{
		var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Customer/");

		var content = new StringContent(JsonSerializer.Serialize(new
		{
			Model = new DeleteCustomerModel(id)
		}), Encoding.UTF8, "application/json");

		request.Content = content;

		var response = await httpClient.SendAsync(request);

		return response.IsSuccessStatusCode
			? JsonSerializer.Deserialize<ApiResponse<CustomerDto>>(
				await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				})
			: new ApiResponse<CustomerDto>
			{
				IsError = true,
				ErrorMessage = await response.Content.ReadAsStringAsync()
			};
	}

	public async Task<ApiResponse<CustomerDto>?> Create(CustomerDto customerDto)
	{
		var response = await httpClient.PostAsJsonAsync("api/Customer", new
		{
			Model = new CreateCustomerModel(customerDto.FirstName!, customerDto.LastName!, customerDto.DateOfBirth!.Value, customerDto.PhoneNumber!, customerDto.Email!, customerDto.BankAccount!)
		});

		if (response.IsSuccessStatusCode)
		{
			return JsonSerializer.Deserialize<ApiResponse<CustomerDto>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});
		}
		return new ApiResponse<CustomerDto>
		{
			IsError = true,
			ErrorMessage = await response.Content.ReadAsStringAsync()
		};
	}

	public Task Update(CustomerDto customerDto)
	{
		throw new NotImplementedException();
	}
}