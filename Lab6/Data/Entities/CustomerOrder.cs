namespace Lab6.Data.Entities;

public class CustomerOrder
{
    public int OrderId { get; set; }
    public string OrderDetails { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public int PaymentMethodCode { get; set; }
    public int OrderStatusCode { get; set; }
    
    public Customer Customer { get; set; }
    public RefPaymentMethod PaymentMethod { get; set; }
    public RefOrderStatusCode OrderStatus { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}