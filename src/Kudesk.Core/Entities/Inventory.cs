namespace Kudesk.Core.Entities;

public class Brand : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Logo { get; set; }
    public bool IsActive { get; set; } = true;
}

public class ProductCategory : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? ParentId { get; set; }
    public ProductCategory? Parent { get; set; }
    public ICollection<ProductCategory> Children { get; set; } = new List<ProductCategory>();
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public bool IsActive { get; set; } = true;
}

public class Unit : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public decimal? BaseQuantity { get; set; }
    public int? ParentId { get; set; }
    public bool IsActive { get; set; } = true;
}

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Sku { get; set; }
    public string? Barcode { get; set; }
    public decimal CostPrice { get; set; }
    public decimal SellPrice { get; set; }
    public decimal? TaxRate { get; set; }
    public int StockQuantity { get; set; }
    public int ReorderLevel { get; set; } = 10;
    public bool IsTrackStock { get; set; } = true;
    public bool IsActive { get; set; } = true;
    public string? ImageUrl { get; set; }

    public int? TenantId { get; set; }
    public Tenant? Tenant { get; set; }
    public int CategoryId { get; set; }
    public ProductCategory? Category { get; set; }
    public int? BrandId { get; set; }
    public Brand? Brand { get; set; }
    public int UnitId { get; set; }
    public Unit? Unit { get; set; }
    public int WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }
}

public class Warehouse : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public bool IsPrimary { get; set; }
    public bool IsActive { get; set; } = true;
}

public class StockAdjustment : BaseEntity
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
    public string Type { get; set; } = "addition";
    public string? Reason { get; set; }
    public int? CreatedById { get; set; }
    public User? CreatedBy { get; set; }
}

public class Supplier : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? ContactPerson { get; set; }
    public bool IsActive { get; set; } = true;
}

public class Purchase : BaseEntity
{
    public string ReferenceNo { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public PurchaseStatus Status { get; set; } = PurchaseStatus.Ordered;
    public string? Notes { get; set; }

    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public ICollection<PurchaseItem> Items { get; set; } = new List<PurchaseItem>();
}

public class PurchaseItem : BaseEntity
{
    public int PurchaseId { get; set; }
    public Purchase? Purchase { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal Subtotal { get; set; }
    public int? UnitId { get; set; }
}

public enum PurchaseStatus
{
    Ordered = 0,
    Received = 1,
    Partial = 2,
    Cancelled = 3
}

public class PurchaseReturn : BaseEntity
{
    public string ReferenceNo { get; set; } = string.Empty;
    public int PurchaseId { get; set; }
    public Purchase? Purchase { get; set; }
    public DateTime ReturnDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public string? Reason { get; set; }
    public ReturnStatus Status { get; set; } = ReturnStatus.Pending;
}

public enum ReturnStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2,
    Completed = 3
}