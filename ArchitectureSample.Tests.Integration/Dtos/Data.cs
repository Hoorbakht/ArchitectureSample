namespace ArchitectureSample.Tests.Integration.Dtos;

public class Data<T> where T : class
{
    public List<T> items { get; set; }

    public int totalItems { get; set; }

    public int page { get; set; }

    public int pageSize { get; set; }
}