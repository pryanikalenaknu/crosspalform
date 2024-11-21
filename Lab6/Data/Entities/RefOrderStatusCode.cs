namespace Lab6.Data.Entities;

public class RefOrderStatusCode
{
    public int OrderStatusCode { get; set; }
    public string OrderStatusDescription { get; set; }
    
    public ICollection<CustomerOrder> CustomerOrders { get; set; }
}