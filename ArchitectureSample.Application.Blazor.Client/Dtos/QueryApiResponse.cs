namespace ArchitectureSample.Application.Blazor.Client.Dtos;

public record QueryApiResponse<T> where T : class
{
	public Data<T>? Data { get; set; }

	public bool IsError { get; set; }

	public object? ErrorMessage { get; set; }
}