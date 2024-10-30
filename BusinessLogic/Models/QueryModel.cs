namespace BusinessLogic.Models;
public class QueryModel
{
    public int? PageNumber { get; set; } = null;
    public int? PageSize { get; set; } = null;

    public string? SortBy { get; set; } = null;

    public bool IsDescending { get; set; } = false;
}
