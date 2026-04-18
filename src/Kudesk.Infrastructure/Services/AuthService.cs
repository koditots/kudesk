using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Kudesk.Core.Entities;

namespace Kudesk.Infrastructure.Services;

public interface IAuthService
{
    Task<User?> LoginAsync(string email, string password);
    Task<User?> LoginWithTenantAsync(string email, string password, int tenantId);
    Task<User> CreateUserAsync(User user, string password);
    Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
    string GenerateToken(User user);
}

public class AuthService : IAuthService
{
    private static readonly Dictionary<string, string> PasswordHashes = new();
    private static readonly Dictionary<string, int> SessionTokens = new();

    public async Task<User?> LoginAsync(string email, string password)
    {
        await Task.CompletedTask;
        return null;
    }

    public async Task<User?> LoginWithTenantAsync(string email, string password, int tenantId)
    {
        await Task.CompletedTask;
        return null;
    }

    public async Task<User> CreateUserAsync(User user, string password)
    {
        user.PasswordHash = HashPassword(password);
        await Task.CompletedTask;
        return user;
    }

    public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
    {
        await Task.CompletedTask;
        return false;
    }

    public string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes).ToLower();
    }

    public bool VerifyPassword(string password, string hash)
    {
        return HashPassword(password) == hash;
    }

    public string GenerateToken(User user)
    {
        var payload = JsonSerializer.Serialize(new { user.Id, user.Email, user.Role, user.TenantId, Exp = DateTimeOffset.UtcNow.AddHours(8).ToUnixTimeSeconds() });
        var bytes = Encoding.UTF8.GetBytes(payload);
        return Convert.ToBase64String(bytes);
    }

    public static bool ValidateToken(string token, out int? userId, out int? tenantId, out UserRole role)
    {
        userId = null;
        tenantId = null;
        role = UserRole.Staff;
        
        try
        {
            var bytes = Convert.FromBase64String(token);
            var json = Encoding.UTF8.GetString(bytes);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            
            if (root.TryGetProperty("userId", out var uid)) userId = uid.GetInt32();
            if (root.TryGetProperty("tenantId", out var tid)) tenantId = tid.GetInt32();
            if (root.TryGetProperty("Role", out var r)) role = (UserRole)r.GetInt32();
            if (root.TryGetProperty("Exp", out var exp))
            {
                var expiry = DateTimeOffset.FromUnixTimeSeconds(exp.GetInt64());
                if (expiry < DateTimeOffset.UtcNow) return false;
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static int? GetCurrentUserId() => null;
    public static int? GetCurrentTenantId() => null;
    public static UserRole GetCurrentRole() => UserRole.Staff;
}

public class CurrentSession
{
    public static int? UserId { get; set; }
    public static int? TenantId { get; set; }
    public static UserRole Role { get; set; } = UserRole.Staff;
    public static string? Token { get; set; }
    public static DateTime LoginAt { get; set; }

    public static bool IsLoggedIn => UserId.HasValue;
    public static bool IsSuperAdmin => Role == UserRole.SuperAdmin;
    public static bool IsAdmin => Role == UserRole.Admin || Role == UserRole.SuperAdmin;
    public static bool IsManager => Role == UserRole.Manager || IsAdmin;
}