using Kudesk.Core.Entities;

namespace Kudesk.Infrastructure.Services;

public interface IPermissionService
{
    bool HasPermission(int? tenantId, int? userId, string module, string action);
    bool IsSuperAdmin(int? userId);
    bool IsAdmin(int? userId, int? tenantId);
    Dictionary<string, Dictionary<string, bool>> GetUserPermissions(int? userId);
    void SetUserPermissions(int userId, Dictionary<string, Dictionary<string, bool>> permissions);
}

public class PermissionService : IPermissionService
{
    private readonly Dictionary<string, Dictionary<string, bool>> _superAdminPermissions = new()
    {
        ["dashboard"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["users"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["tenants"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["subscriptions"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["transactions"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["enquiries"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["languages"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["currencies"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["products"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["sales"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["purchases"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["reports"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["settings"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true }
    };

    private readonly Dictionary<string, Dictionary<string, bool>> _adminPermissions = new()
    {
        ["dashboard"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["products"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["categories"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["brands"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["warehouses"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["units"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["suppliers"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["customers"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["sales"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["sales_returns"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["purchases"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["purchase_returns"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["expenses"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["reports"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
        ["settings"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true }
    };

    private readonly Dictionary<string, Dictionary<string, bool>> _managerPermissions = new()
    {
        ["dashboard"] = new() { ["view"] = true },
        ["products"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = false },
        ["categories"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = false },
        ["brands"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = false },
        ["suppliers"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = false },
        ["customers"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = false },
        ["sales"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = false },
        ["purchases"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = false },
        ["expenses"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = false },
        ["reports"] = new() { ["view"] = true }
    };

    private readonly Dictionary<string, Dictionary<string, bool>> _staffPermissions = new()
    {
        ["dashboard"] = new() { ["view"] = true },
        ["products"] = new() { ["view"] = true, ["create"] = false, ["edit"] = false, ["delete"] = false },
        ["categories"] = new() { ["view"] = true, ["create"] = false, ["edit"] = false, ["delete"] = false },
        ["customers"] = new() { ["view"] = true, ["create"] = false, ["edit"] = false, ["delete"] = false },
        ["sales"] = new() { ["view"] = true, ["create"] = true, ["edit"] = false, ["delete"] = false },
        ["expenses"] = new() { ["view"] = true, ["create"] = false, ["edit"] = false, ["delete"] = false },
        ["reports"] = new() { ["view"] = true }
    };

    private static readonly Dictionary<string, Dictionary<string, bool>> EmptyPermissions = new();

    public bool HasPermission(int? tenantId, int? userId, string module, string action)
    {
        var perms = GetUserPermissions(userId);
        if (!perms.TryGetValue(module, out var modulePerms)) return false;
        return modulePerms.TryGetValue(action, out var hasAction) && hasAction;
    }

    public bool IsSuperAdmin(int? userId)
    {
        return userId.HasValue;
    }

    public bool IsAdmin(int? userId, int? tenantId)
    {
        return userId.HasValue && tenantId.HasValue;
    }

    public Dictionary<string, Dictionary<string, bool>> GetUserPermissions(int? userId)
    {
        if (!userId.HasValue) return EmptyPermissions;
        return EmptyPermissions;
    }

    public void SetUserPermissions(int userId, Dictionary<string, Dictionary<string, bool>> permissions)
    {
    }
}

public static class PermissionExtensions
{
    public static Dictionary<string, Dictionary<string, bool>> GetPermissionsForRole(UserRole role)
    {
        var service = new PermissionService();
        return role switch
        {
            UserRole.SuperAdmin => new Dictionary<string, Dictionary<string, bool>>
            {
                ["dashboard"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
                ["users"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
                ["tenants"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
                ["subscriptions"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
                ["transactions"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
                ["products"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
                ["sales"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
                ["reports"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
                ["settings"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true }
            },
            UserRole.Admin => new Dictionary<string, Dictionary<string, bool>>
            {
                ["dashboard"] = new() { ["view"] = true },
                ["products"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
                ["sales"] = new() { ["view"] = true, ["create"] = true, ["edit"] = true, ["delete"] = true },
                ["reports"] = new() { ["view"] = true },
                ["settings"] = new() { ["view"] = true, ["edit"] = true }
            },
            UserRole.Manager => new Dictionary<string, Dictionary<string, bool>>
            {
                ["dashboard"] = new() { ["view"] = true },
                ["products"] = new() { ["view"] = true, ["edit"] = true },
                ["sales"] = new() { ["view"] = true, ["create"] = true },
                ["reports"] = new() { ["view"] = true }
            },
            UserRole.Staff => new Dictionary<string, Dictionary<string, bool>>
            {
                ["dashboard"] = new() { ["view"] = true },
                ["products"] = new() { ["view"] = true },
                ["sales"] = new() { ["view"] = true, ["create"] = true }
            },
            _ => new()
        };
    }

    public static bool Can(this Dictionary<string, Dictionary<string, bool>> permissions, string module, string action)
    {
        if (!permissions.TryGetValue(module, out var modulePerms)) return false;
        return modulePerms.TryGetValue(action, out var allowed) && allowed;
    }

    public static List<string> GetAccessibleModules(this Dictionary<string, Dictionary<string, bool>> permissions)
    {
        return permissions.Keys.ToList();
    }

    public static bool HasAnyAction(this Dictionary<string, Dictionary<string, bool>> permissions, string module)
    {
        return permissions.TryGetValue(module, out var modulePerms) && modulePerms.Values.Any(v => v);
    }
}