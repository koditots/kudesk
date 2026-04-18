namespace Kudesk.Core.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public SyncStatus SyncStatus { get; set; } = SyncStatus.Pending;
}

public enum SyncStatus
{
    Pending = 0,
    Synced = 1,
    Failed = 2
}