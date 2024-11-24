namespace Lab5.Models;

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int ProductTypeCode { get; set; }
    
    public RefProductType ProductType { get; set; }
}