using Kudesk.Core.Entities;

namespace Kudesk.Infrastructure.Services;

public class ReportService
{
    public SalesReport GetSalesReport(int? tenantId, DateTime fromDate, DateTime toDate)
    {
        var report = new SalesReport
        {
            TenantId = tenantId,
            FromDate = fromDate,
            ToDate = toDate,
            TotalSales = 0,
            TotalTax = 0,
            TotalDiscount = 0,
            GrossTotal = 0,
            TransactionCount = 0,
            AverageSale = 0,
            TopProducts = new List<TopProduct>(),
            SalesByDay = new Dictionary<DateTime, decimal>()
        };
        return report;
    }

    public PurchaseReport GetPurchaseReport(int? tenantId, DateTime fromDate, DateTime toDate)
    {
        return new PurchaseReport
        {
            TenantId = tenantId,
            FromDate = fromDate,
            ToDate = toDate,
            TotalPurchases = 0,
            TotalTax = 0,
            TotalDiscount = 0,
            GrandTotal = 0,
            OrderCount = 0,
            AveragePurchase = 0,
            TopSuppliers = new List<TopSupplier>()
        };
    }

    public StockReport GetStockReport(int? tenantId)
    {
        return new StockReport
        {
            TenantId = tenantId,
            TotalProducts = 0,
            TotalValue = 0,
            LowStockItems = new List<LowStockItem>(),
            OutOfStockItems = new List<OutOfStockItem>(),
            OverstockItems = new List<OverstockItem>()
        };
    }

    public ProfitLossReport GetProfitLossReport(int? tenantId, DateTime fromDate, DateTime toDate)
    {
        return new ProfitLossReport
        {
            TenantId = tenantId,
            FromDate = fromDate,
            ToDate = toDate,
            TotalIncome = 0,
            TotalCostOfGoods = 0,
            GrossProfit = 0,
            TotalExpenses = 0,
            NetProfit = 0,
            ProfitMargin = 0
        };
    }

    public DashboardReport GetDashboardReport(int? tenantId)
    {
        return new DashboardReport
        {
            TenantId = tenantId,
            TodaySales = 0,
            TodayExpenses = 0,
            TodayProfit = 0,
            WeekSales = new Dictionary<DateTime, decimal>(),
            MonthSales = new Dictionary<DateTime, decimal>(),
            TopSellingProducts = new List<TopProduct>(),
            LowStockAlerts = new List<LowStockItem>()
        };
    }
}

public class SalesReport
{
    public int? TenantId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public decimal TotalSales { get; set; }
    public decimal TotalTax { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal GrossTotal { get; set; }
    public int TransactionCount { get; set; }
    public decimal AverageSale { get; set; }
    public List<TopProduct> TopProducts { get; set; } = new();
    public Dictionary<DateTime, decimal> SalesByDay { get; set; } = new();
}

public class TopProduct
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int QuantitySold { get; set; }
    public decimal Revenue { get; set; }
}

public class PurchaseReport
{
    public int? TenantId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public decimal TotalPurchases { get; set; }
    public decimal TotalTax { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal GrandTotal { get; set; }
    public int OrderCount { get; set; }
    public decimal AveragePurchase { get; set; }
    public List<TopSupplier> TopSuppliers { get; set; } = new();
}

public class TopSupplier
{
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public decimal TotalPurchases { get; set; }
    public int OrderCount { get; set; }
}

public class StockReport
{
    public int? TenantId { get; set; }
    public int TotalProducts { get; set; }
    public decimal TotalValue { get; set; }
    public List<LowStockItem> LowStockItems { get; set; } = new();
    public List<OutOfStockItem> OutOfStockItems { get; set; } = new();
    public List<OverstockItem> OverstockItems { get; set; } = new();
}

public class LowStockItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int CurrentStock { get; set; }
    public int ReorderLevel { get; set; }
}

public class OutOfStockItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int LastRestocked { get; set; }
}

public class OverstockItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int CurrentStock { get; set; }
    public int OptimalStock { get; set; }
}

public class ProfitLossReport
{
    public int? TenantId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalCostOfGoods { get; set; }
    public decimal GrossProfit { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal NetProfit { get; set; }
    public decimal ProfitMargin { get; set; }
}

public class DashboardReport
{
    public int? TenantId { get; set; }
    public decimal TodaySales { get; set; }
    public decimal TodayExpenses { get; set; }
    public decimal TodayProfit { get; set; }
    public Dictionary<DateTime, decimal> WeekSales { get; set; } = new();
    public Dictionary<DateTime, decimal> MonthSales { get; set; } = new();
    public List<TopProduct> TopSellingProducts { get; set; } = new();
    public List<LowStockItem> LowStockAlerts { get; set; } = new();
}

public static class ReportFilterExtensions
{
    public static IQueryable<Sale> BetweenDates(this IQueryable<Sale> query, DateTime fromDate, DateTime toDate)
    {
        return query.Where(s => s.SaleDate >= fromDate && s.SaleDate <= toDate);
    }

    public static IQueryable<Sale> Today(this IQueryable<Sale> query)
    {
        var today = DateTime.Today;
        return query.Where(s => s.SaleDate.Date == today);
    }

    public static IQueryable<Sale> ThisWeek(this IQueryable<Sale> query)
    {
        var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
        return query.Where(s => s.SaleDate >= startOfWeek);
    }

    public static IQueryable<Sale> ThisMonth(this IQueryable<Sale> query)
    {
        var startOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        return query.Where(s => s.SaleDate >= startOfMonth);
    }

    public static IQueryable<Purchase> BetweenDates(this IQueryable<Purchase> query, DateTime fromDate, DateTime toDate)
    {
        return query.Where(p => p.PurchaseDate >= fromDate && p.PurchaseDate <= toDate);
    }

    public static IQueryable<Expense> BetweenDates(this IQueryable<Expense> query, DateTime fromDate, DateTime toDate)
    {
        return query.Where(e => e.ExpenseDate >= fromDate && e.ExpenseDate <= toDate);
    }
}