namespace Lab6.Data.Entities;

public class DepartmentStoreChain
{
    public int DeptStoreChainId { get; set; }
    public string ChainName { get; set; }
    public string ChainDetails { get; set; }
    
    public ICollection<DepartmentStore> DepartmentStores { get; set; }
}
