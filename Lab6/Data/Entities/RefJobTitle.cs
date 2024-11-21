namespace Lab6.Data.Entities;

public class RefJobTitle
{
    public int JobTitleCode { get; set; }
    public string JobTitleDescription { get; set; }
    
    public ICollection<Staff> Staff { get; set; }
}