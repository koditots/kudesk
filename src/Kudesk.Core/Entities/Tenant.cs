namespace Kudesk.Core.Entities;

public class Tenant : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? Logo { get; set; }
    public SubscriptionStatus SubscriptionStatus { get; set; } = SubscriptionStatus.Trial;
    public DateTime? SubscriptionExpiresAt { get; set; }
    public string? PlanId { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Timezone { get; set; } = "UTC";
    public string? Currency { get; set; } = "USD";
    public string? Language { get; set; } = "en";
    public string? TaxNumber { get; set; }
    public string? InvoicePrefix { get; set; } = "INV";
    public string? InvoiceLogo { get; set; }
    public string? InvoiceFooter { get; set; }
    public bool TaxEnabled { get; set; }
    public decimal? TaxRate { get; set; }
}

public enum SubscriptionStatus
{
    Trial = 0,
    Active = 1,
    Suspended = 2,
    Cancelled = 3,
    Expired = 4
}

public class BusinessSetting : BaseEntity
{
    public int TenantId { get; set; }
    public Tenant? Tenant { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}

public class TaxSetting : BaseEntity
{
    public int TenantId { get; set; }
    public Tenant? Tenant { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public bool IsActive { get; set; } = true;
}

public class InvoiceSetting : BaseEntity
{
    public int TenantId { get; set; }
    public Tenant? Tenant { get; set; }
    public string? Header { get; set; }
    public string? Footer { get; set; }
    public string? Terms { get; set; }
    public bool ShowLogo { get; set; } = true;
    public bool ShowBarcode { get; set; }
}