namespace Lab6.Data.Entities;

public class RefProductType
{
    public int ProductTypeCode { get; set; }
    public string ProductTypeDescription { get; set; }
    
    public ICollection<Product> Products { get; set; }
}