namespace Lab6.Data.Entities;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    
    public CustomerOrder CustomerOrder { get; set; }
    public Product Product { get; set; }
}