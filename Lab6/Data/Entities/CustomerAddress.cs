namespace Lab6.Data.Entities;

public class CustomerAddress
{
    public int CustomerId { get; set; }
    public int AddressId { get; set; }
    public DateTime DateFrom { get; set; }
    
    public Customer Customer { get; set; }
    public Address Address { get; set; }
}
