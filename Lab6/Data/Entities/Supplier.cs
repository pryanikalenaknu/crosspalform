namespace Lab6.Data.Entities;

public class Supplier
{
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public string SupplierDetails { get; set; }
    
    public ICollection<SupplierAddress> SupplierAddresses { get; set; }
    public ICollection<ProductSupplier> ProductSuppliers { get; set; }
}