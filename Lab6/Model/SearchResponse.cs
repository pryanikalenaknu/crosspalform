namespace Lab6.Model;

public class SearchResponse
{
    public int OrderId { get; set; }
    public string OrderDetails { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public int PaymentMethodCode { get; set; }
    public int OrderStatusCode { get; set; }

    public string OrderStatus { get; set; }

    public string FirstProductName { get; set; }

}