namespace Kudesk.Core.Entities;

public class Payment : BaseEntity
{
    public int SaleId { get; set; }
    public Sale? Sale { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
}

public enum PaymentMethod
{
    Cash = 0,
    Card = 1,
    Transfer = 2
}