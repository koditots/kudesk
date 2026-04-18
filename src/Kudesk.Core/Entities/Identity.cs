namespace Kudesk.Core.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public UserRole Role { get; set; } = UserRole.Staff;
    public int? TenantId { get; set; }
    public Tenant? Tenant { get; set; }
    public bool IsActive { get; set; } = true;
    public string? PermissionsJson { get; set; }
    public DateTime? LastLoginAt { get; set; }
}

public enum UserRole
{
    SuperAdmin = 0,
    Admin = 1,
    Manager = 2,
    Staff = 3
}

public class SubscriptionPlan : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string Interval { get; set; } = "month";
    public int IntervalCount { get; set; } = 1;
    public int MaxUsers { get; set; } = 5;
    public int MaxProducts { get; set; } = 1000;
    public bool IsActive { get; set; } = true;
    public int SortOrder { get; set; }
    public string? FeaturesJson { get; set; }
}

public class PaymentTransaction : BaseEntity
{
    public int TenantId { get; set; }
    public Tenant? Tenant { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public string? Method { get; set; }
    public string? Gateway { get; set; }
    public string? TransactionId { get; set; }
    public string? InvoiceId { get; set; }
    public DateTime? PaidAt { get; set; }
}

public enum PaymentStatus
{
    Pending = 0,
    Completed = 1,
    Failed = 2,
    Refunded = 3
}

public class Enquiry : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public EnquiryStatus Status { get; set; } = EnquiryStatus.Open;
    public int? AssignedToId { get; set; }
    public User? AssignedTo { get; set; }
    public DateTime? ResolvedAt { get; set; }
}

public enum EnquiryStatus
{
    Open = 0,
    InProgress = 1,
    Resolved = 2,
    Closed = 3
}

public class Language : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; } = true;
}

public class Currency : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public decimal Rate { get; set; } = 1;
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; } = true;
}