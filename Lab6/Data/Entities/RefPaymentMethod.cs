namespace Lab6.Data.Entities;

public class RefPaymentMethod
{
    public int PaymentMethodCode { get; set; }
    public string PaymentMethodDescription { get; set; }
    
    public ICollection<CustomerOrder> CustomerOrders { get; set; }
}