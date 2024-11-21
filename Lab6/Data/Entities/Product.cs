namespace Lab6.Data.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int ProductTypeCode { get; set; }
    
    public RefProductType ProductType { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public ICollection<ProductSupplier> ProductSuppliers { get; set; }
}
