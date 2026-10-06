namespace NoBeard.Learn.AspNet.WebApp.Data.Models;

public class MovieQueryParameters : QueryParameters
{
    public string? Title { get; set; }

    public int? ReleaseYear { get; set; }
}
