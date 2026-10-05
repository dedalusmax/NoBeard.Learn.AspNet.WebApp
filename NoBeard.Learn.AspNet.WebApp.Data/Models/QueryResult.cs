namespace NoBeard.Learn.AspNet.WebApp.Data.Models;

public class QueryResult<TEntity>
{
    public IReadOnlyCollection<TEntity> Items { get; init; } = [];

    public int TotalCount { get; init; }

    public int PageNumber { get; init; }

    public int PageSize { get; init; }

    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
