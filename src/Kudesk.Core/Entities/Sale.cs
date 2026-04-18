namespace Kudesk.Core.Entities;

public class Sale : BaseEntity
{
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; }
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}