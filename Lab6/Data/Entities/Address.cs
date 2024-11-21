namespace Lab6.Data.Entities;

public class Address
{
    public int AddressId { get; set; }
    public string AddressDetails { get; set; }
    
    public ICollection<SupplierAddress> SupplierAddresses { get; set; }
    public ICollection<CustomerAddress> CustomerAddresses { get; set; }
    public ICollection<DepartmentStore> DepartmentStores { get; set; }
}