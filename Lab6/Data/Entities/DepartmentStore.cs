namespace Lab6.Data.Entities;

public class DepartmentStore
{
    public int DeptStoreId { get; set; }
    public int DeptStoreChainId { get; set; }
    public int AddressId { get; set; }
    public string StoreDetails { get; set; }
    
    public DepartmentStoreChain DepartmentStoreChain { get; set; }
    public Address Address { get; set; }
    public ICollection<Staff> Staff { get; set; }
}
