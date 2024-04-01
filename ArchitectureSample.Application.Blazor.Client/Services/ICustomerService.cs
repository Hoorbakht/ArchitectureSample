using ArchitectureSample.Application.Blazor.Client.Dtos;

namespace ArchitectureSample.Application.Blazor.Client.Services;

public interface ICustomerService
{
	Task<QueryApiResponse<CustomerDto>?> Get(int page);

	Task<CustomerDto> Get(Guid id);

	Task Delete(Guid id);

	Task<HttpResponseMessage> Create(CustomerDto customerDto);

	Task Update(CustomerDto customerDto);
}