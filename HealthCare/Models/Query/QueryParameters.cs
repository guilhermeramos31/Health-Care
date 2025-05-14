namespace HealthCare.Models.Query;

public abstract class QueryParameters
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string? Search { get; set; }
    public Filter Filter { get; set; }
}
