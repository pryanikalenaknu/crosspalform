namespace Lab5.Models;

public class SearchModel
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<int>? ProductIds { get; set; }
    public string? OrderStart { get; set; }
    public string? OrderEnd { get; set; }
}