namespace Lab6.Data.Entities;

public class ProductSupplier
{
    public int ProductId { get; set; }
    public int SupplierId { get; set; }
    public decimal TotalSupplied { get; set; }
    
    public Product Product { get; set; }
    public Supplier Supplier { get; set; }
}
