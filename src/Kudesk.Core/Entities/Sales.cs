namespace Kudesk.Core.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? TaxNumber { get; set; }
    public CustomerType Type { get; set; } = CustomerType.Retail;
    public bool IsActive { get; set; } = true;
    public decimal? CreditLimit { get; set; }
    public decimal Balance { get; set; }
    
    public int? TenantId { get; set; }
    public Tenant? Tenant { get; set; }
}

public enum CustomerType
{
    Retail = 0,
    Wholesale = 1,
    Vip = 2
}

public enum SaleStatus
{
    Draft = 0,
    Completed = 1,
    Refunded = 2,
    Cancelled = 3
}

public class Sale : BaseEntity
{
    public string InvoiceNo { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;
    public decimal Subtotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal ChangeAmount { get; set; }
    public SaleStatus Status { get; set; } = SaleStatus.Completed;
    public string? Notes { get; set; }
    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;

    public int? TenantId { get; set; }
    public Tenant? Tenant { get; set; }
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public int? CreatedById { get; set; }
    public User? CreatedBy { get; set; }
    public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}

public class SaleItem : BaseEntity
{
    public int SaleId { get; set; }
    public Sale? Sale { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TaxRate { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal Subtotal { get; set; }
}

public class Payment : BaseEntity
{
    public int SaleId { get; set; }
    public Sale? Sale { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public string? ReferenceNo { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
}

public enum PaymentMethod
{
    Cash = 0,
    Card = 1,
    Transfer = 2,
    Credit = 3
}

public class SaleReturn : BaseEntity
{
    public string ReferenceNo { get; set; } = string.Empty;
    public int SaleId { get; set; }
    public Sale? Sale { get; set; }
    public DateTime ReturnDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public string? Reason { get; set; }
    public ReturnStatus Status { get; set; } = ReturnStatus.Pending;
}

public class SaleReturnItem : BaseEntity
{
    public int SaleReturnId { get; set; }
    public SaleReturn? SaleReturn { get; set; }
    public int SaleItemId { get; set; }
    public int Quantity { get; set; }
    public decimal Amount { get; set; }
}

public class ExpenseCategory : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}

public class Expense : BaseEntity
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime ExpenseDate { get; set; } = DateTime.UtcNow;
    public string? ReferenceNo { get; set; }
    public string? Notes { get; set; }
    public int CategoryId { get; set; }
    public ExpenseCategory? Category { get; set; }
    public int? CreatedById { get; set; }
    public User? CreatedBy { get; set; }
}

public class AppSetting : BaseEntity
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}