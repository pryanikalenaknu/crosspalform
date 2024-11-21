using System.Text.Json.Serialization;

namespace Lab5.Models;

public class CustomerOrderSearchResultModel
{
    public int OrderId { get; set; }
    public string OrderDetails { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public int PaymentMethodCode { get; set; }
    public int OrderStatusCode { get; set; }
    
    public RefOrderStatusCode OrderStatus { get; set; }

    public string FirstProductName;
}