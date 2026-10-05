namespace NoBeard.Learn.AspNet.WebApp.Data.Models;

public class QueryParameters
{
    public string? Search { get; set; }

    public string? SortBy { get; set; }

    public bool Descending { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 5;
}
