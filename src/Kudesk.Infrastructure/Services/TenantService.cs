using Kudesk.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kudesk.Infrastructure.Services;

public interface ITenantService
{
    int? GetCurrentTenantId();
    void SetCurrentTenant(int tenantId);
    IQueryable<T> ApplyTenantFilter<T>(IQueryable<T> query) where T : class;
    bool CanAccess(int? tenantId);
    void ClearCurrentTenant();
}

public class TenantService : ITenantService
{
    private static int? _currentTenantId;
    private static int? _currentUserId;

    public int? GetCurrentTenantId()
    {
        return _currentTenantId;
    }

    public void SetCurrentTenant(int tenantId)
    {
        _currentTenantId = tenantId;
    }

    public void ClearCurrentTenant()
    {
        _currentTenantId = null;
    }

    public IQueryable<T> ApplyTenantFilter<T>(IQueryable<T> query) where T : class
    {
        if (!_currentTenantId.HasValue) return query;

        var tenantProperty = typeof(T).GetProperty("TenantId");
        if (tenantProperty != null && tenantProperty.PropertyType == typeof(int?))
        {
            return query.Where(e => EF.Property<int?>(e, "TenantId") == _currentTenantId);
        }
        return query;
    }

    public bool CanAccess(int? tenantId)
    {
        if (!_currentTenantId.HasValue) return true;
        return tenantId == _currentTenantId;
    }
}

public static class TenantQueryExtensions
{
    public static IQueryable<Product> ForTenant(this IQueryable<Product> query, int? tenantId)
    {
        return tenantId.HasValue ? query.Where(p => p.TenantId == tenantId) : query;
    }

    public static IQueryable<Sale> ForTenant(this IQueryable<Sale> query, int? tenantId)
    {
        return tenantId.HasValue ? query.Where(s => s.TenantId == tenantId) : query;
    }

    public static IQueryable<Customer> ForTenant(this IQueryable<Customer> query, int? tenantId)
    {
        return tenantId.HasValue ? query.Where(c => c.TenantId == tenantId) : query;
    }
}

public class MultiTenantContext
{
    private static TenantService? _instance;

    public static void Initialize(TenantService instance)
    {
        _instance = instance;
    }

    public static int? TenantId => _instance?.GetCurrentTenantId();

    public static bool IsSingleTenant => TenantId.HasValue;

    public static void ExecuteAsTenant(int tenantId, Action action)
    {
        var previous = _instance?.GetCurrentTenantId();
        _instance?.SetCurrentTenant(tenantId);
        try
        {
            action();
        }
        finally
        {
            if (previous.HasValue)
                _instance?.SetCurrentTenant(previous.Value);
            else
                _instance?.ClearCurrentTenant();
        }
    }

    public static T ExecuteAsTenant<T>(int tenantId, Func<T> func)
    {
        var previous = _instance?.GetCurrentTenantId();
        _instance?.SetCurrentTenant(tenantId);
        try
        {
            return func();
        }
        finally
        {
            if (previous.HasValue)
                _instance?.SetCurrentTenant(previous.Value);
            else
                _instance?.ClearCurrentTenant();
        }
    }
}