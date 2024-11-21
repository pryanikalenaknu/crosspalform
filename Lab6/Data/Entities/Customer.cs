namespace Lab6.Data.Entities;

public class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string ContactDetails { get; set; }
    
    public ICollection<CustomerOrder> CustomerOrders { get; set; }
    public ICollection<CustomerAddress> CustomerAddresses { get; set; }
}