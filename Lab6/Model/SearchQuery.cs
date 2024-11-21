namespace Lab6.Model;

public class SearchQuery
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<int>? ProductIds { get; set; }
    public string? OrderStart { get; set; }
    public string? OrderEnd { get; set; }
}