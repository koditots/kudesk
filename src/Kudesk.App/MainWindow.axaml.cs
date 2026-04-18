using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;

namespace Kudesk.App;

public partial class MainWindow : Window
{
    private TabControl? _tabControl;
    private TextBlock? _todaySalesText;
    private TextBlock? _productsText;
    private TextBlock? _customersText;
    private TextBlock? _lowStockText;
    private decimal _cartTotal = 0;
    private int _cartItemCount = 0;

    public MainWindow()
    {
        InitializeComponent();
        
        Loaded += (s, e) =>
        {
            _tabControl = this.FindControl<TabControl>("MainTabs");
            _todaySalesText = this.FindControl<TextBlock>("TodaySalesValue");
            _productsText = this.FindControl<TextBlock>("ProductsValue");
            _customersText = this.FindControl<TextBlock>("CustomersValue");
            _lowStockText = this.FindControl<TextBlock>("LowStockValue");
        };
    }

    private void NavigateToTab(object? sender, RoutedEventArgs e)
    {
        if (sender is Button btn && _tabControl != null)
        {
            var tag = btn.Tag?.ToString();
            var index = tag switch
            {
                "superadmin" => 0,
                "admin" => 1,
                "pos" => 2,
                "inventory" => 3,
                "purchases" => 4,
                "reports" => 5,
                "settings" => 6,
                _ => 0
            };
            _tabControl.SelectedIndex = index;
        }
    }

    private void ShowSuperAdmin(object? sender, RoutedEventArgs e)
    {
        if (_tabControl != null) _tabControl.SelectedIndex = 0;
    }

    private void ShowAdmin(object? sender, RoutedEventArgs e)
    {
        if (_tabControl != null) _tabControl.SelectedIndex = 1;
    }

    private void ShowPOS(object? sender, RoutedEventArgs e)
    {
        if (_tabControl != null) _tabControl.SelectedIndex = 2;
    }

    private void ShowInventory(object? sender, RoutedEventArgs e)
    {
        if (_tabControl != null) _tabControl.SelectedIndex = 3;
    }

    private void ShowPurchases(object? sender, RoutedEventArgs e)
    {
        if (_tabControl != null) _tabControl.SelectedIndex = 4;
    }

    private void ShowReports(object? sender, RoutedEventArgs e)
    {
        if (_tabControl != null) _tabControl.SelectedIndex = 5;
    }

    private void ShowSettings(object? sender, RoutedEventArgs e)
    {
        if (_tabControl != null) _tabControl.SelectedIndex = 6;
    }

    private void ExitApp(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void AddProduct(object? sender, RoutedEventArgs e)
    {
        var dialog = new Window
        {
            Title = "Add Product",
            Width = 500,
            Height = 450,
            Background = new SolidColorBrush(Color.Parse("#252526"))
        };
        
        var panel = new StackPanel { Margin = new Avalonia.Thickness(20), Spacing = 15 };
        
        panel.Children.Add(new TextBlock { Text = "Add New Product", FontSize = 20, FontWeight = FontWeight.Bold, Foreground = Brushes.White });
        
        panel.Children.Add(new TextBlock { Text = "Product Name", Foreground = Brushes.White });
        panel.Children.Add(new TextBox { Watermark = "Enter product name" });
        
        panel.Children.Add(new TextBlock { Text = "SKU", Foreground = Brushes.White });
        panel.Children.Add(new TextBox { Watermark = "Auto-generated if empty" });
        
        panel.Children.Add(new TextBlock { Text = "Category", Foreground = Brushes.White });
        panel.Children.Add(new ComboBox { Width = 200 });
        
        panel.Children.Add(new TextBlock { Text = "Price", Foreground = Brushes.White });
        panel.Children.Add(new NumericUpDown { Minimum = 0, Maximum = 999999, Increment = 0.01m, FormatString = "F2" });
        
        panel.Children.Add(new TextBlock { Text = "Stock Quantity", Foreground = Brushes.White });
        panel.Children.Add(new NumericUpDown { Minimum = 0, Maximum = 999999 });
        
        var saveBtn = new Button { Content = "Save Product", HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center, Width = 150, Margin = new Avalonia.Thickness(0, 20, 0, 0) };
        saveBtn.Click += (s, args) => dialog.Close();
        panel.Children.Add(saveBtn);
        
        dialog.Content = panel;
        dialog.ShowDialog(this);
    }

    private void AddCategory(object? sender, RoutedEventArgs e)
    {
        var dialog = new Window
        {
            Title = "Add Category",
            Width = 400,
            Height = 200,
            Background = new SolidColorBrush(Color.Parse("#252526"))
        };
        
        var panel = new StackPanel { Margin = new Avalonia.Thickness(20), Spacing = 15 };
        panel.Children.Add(new TextBlock { Text = "Category Name", Foreground = Brushes.White });
        panel.Children.Add(new TextBox { Watermark = "Enter category name" });
        panel.Children.Add(new Button { Content = "Save", Width = 100, HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center });
        
        dialog.Content = panel;
        dialog.ShowDialog(this);
    }

    private void AddBrand(object? sender, RoutedEventArgs e)
    {
        var dialog = new Window { Title = "Add Brand", Width = 400, Height = 200, Background = new SolidColorBrush(Color.Parse("#252526")) };
        var panel = new StackPanel { Margin = new Avalonia.Thickness(20), Spacing = 15 };
        panel.Children.Add(new TextBlock { Text = "Brand Name", Foreground = Brushes.White });
        panel.Children.Add(new TextBox { Watermark = "Enter brand name" });
        dialog.Content = panel;
        dialog.ShowDialog(this);
    }

    private void AddWarehouse(object? sender, RoutedEventArgs e)
    {
        var dialog = new Window { Title = "Add Warehouse", Width = 400, Height = 200, Background = new SolidColorBrush(Color.Parse("#252526")) };
        var panel = new StackPanel { Margin = new Avalonia.Thickness(20), Spacing = 15 };
        panel.Children.Add(new TextBlock { Text = "Warehouse Name", Foreground = Brushes.White });
        panel.Children.Add(new TextBox { Watermark = "Enter warehouse name" });
        dialog.Content = panel;
        dialog.ShowDialog(this);
    }

    private void GenerateSalesReport(object? sender, RoutedEventArgs e)
    {
        var dialog = new Window
        {
            Title = "Sales Report",
            Width = 500,
            Height = 350,
            Background = new SolidColorBrush(Color.Parse("#252526"))
        };
        
        var panel = new StackPanel { Margin = new Avalonia.Thickness(20), Spacing = 15 };
        panel.Children.Add(new TextBlock { Text = "Sales Report", FontSize = 24, FontWeight = FontWeight.Bold, Foreground = Brushes.White });
        panel.Children.Add(new TextBlock { Text = "Date Range: Today", Foreground = Brushes.Gray });
        panel.Children.Add(new TextBlock { Text = "Total Sales: $1,234.56", Foreground = new SolidColorBrush(Color.Parse("#4EC9B0")), FontSize = 18 });
        panel.Children.Add(new TextBlock { Text = "Transactions: 45", Foreground = Brushes.White });
        panel.Children.Add(new TextBlock { Text = "Average: $27.43", Foreground = Brushes.White });
        
        var closeBtn = new Button { Content = "Close", Width = 100, HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right, Margin = new Avalonia.Thickness(0, 20, 0, 0) };
        closeBtn.Click += (s, args) => dialog.Close();
        panel.Children.Add(closeBtn);
        
        dialog.Content = panel;
        dialog.ShowDialog(this);
    }

    private void GeneratePurchaseReport(object? sender, RoutedEventArgs e)
    {
        var dialog = new Window
        {
            Title = "Purchase Report",
            Width = 500,
            Height = 300,
            Background = new SolidColorBrush(Color.Parse("#252526"))
        };
        
        var panel = new StackPanel { Margin = new Avalonia.Thickness(20), Spacing = 15 };
        panel.Children.Add(new TextBlock { Text = "Purchase Report", FontSize = 24, FontWeight = FontWeight.Bold, Foreground = Brushes.White });
        panel.Children.Add(new TextBlock { Text = "Total Purchases: $890.00", Foreground = new SolidColorBrush(Color.Parse("#569CD6")), FontSize = 18 });
        panel.Children.Add(new TextBlock { Text = "Orders: 12", Foreground = Brushes.White });
        
        var closeBtn = new Button { Content = "Close", Width = 100, HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right };
        closeBtn.Click += (s, args) => dialog.Close();
        panel.Children.Add(closeBtn);
        
        dialog.Content = panel;
        dialog.ShowDialog(this);
    }

    private void GenerateStockReport(object? sender, RoutedEventArgs e)
    {
        var dialog = new Window
        {
            Title = "Stock Report",
            Width = 500,
            Height = 300,
            Background = new SolidColorBrush(Color.Parse("#252526"))
        };
        
        var panel = new StackPanel { Margin = new Avalonia.Thickness(20), Spacing = 15 };
        panel.Children.Add(new TextBlock { Text = "Stock Report", FontSize = 24, FontWeight = FontWeight.Bold, Foreground = Brushes.White });
        panel.Children.Add(new TextBlock { Text = "Total Products: 156", Foreground = Brushes.White });
        panel.Children.Add(new TextBlock { Text = "Total Value: $12,450.00", Foreground = new SolidColorBrush(Color.Parse("#DCDCAA")), FontSize = 18 });
        panel.Children.Add(new TextBlock { Text = "Low Stock: 3 items", Foreground = new SolidColorBrush(Color.Parse("#F44747")) });
        
        var closeBtn = new Button { Content = "Close", Width = 100, HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right };
        closeBtn.Click += (s, args) => dialog.Close();
        panel.Children.Add(closeBtn);
        
        dialog.Content = panel;
        dialog.ShowDialog(this);
    }

    private void ClearCart(object? sender, RoutedEventArgs e)
    {
        _cartTotal = 0;
        _cartItemCount = 0;
        
        var cartText = this.FindControl<TextBlock>("CartItems");
        if (cartText != null) cartText.Text = "Cart (0 items)";
        
        var totalText = this.FindControl<TextBlock>("CartTotal");
        if (totalText != null) totalText.Text = "$0.00";
    }

    private void Checkout(object? sender, RoutedEventArgs e)
    {
        if (_cartTotal == 0) return;
        
        var dialog = new Window
        {
            Title = "Checkout Complete",
            Width = 400,
            Height = 250,
            Background = new SolidColorBrush(Color.Parse("#252526"))
        };
        
        var panel = new StackPanel { Margin = new Avalonia.Thickness(20), Spacing = 15, HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center };
        panel.Children.Add(new TextBlock { Text = "Sale Complete!", FontSize = 24, FontWeight = FontWeight.Bold, Foreground = new SolidColorBrush(Color.Parse("#4EC9B0")) });
        panel.Children.Add(new TextBlock { Text = "Invoice: INV-" + DateTime.Now.ToString("yyyyMMdd") + "-0001", Foreground = Brushes.White });
        panel.Children.Add(new TextBlock { Text = $"Total: ${_cartTotal:N2}", Foreground = new SolidColorBrush(Color.Parse("#4EC9B0")), FontSize = 18 });
        
        var closeBtn = new Button { Content = "Done", Width = 100, Margin = new Avalonia.Thickness(0, 10, 0, 0) };
        closeBtn.Click += (s, args) =>
        {
            dialog.Close();
            ClearCart(sender, e);
        };
        panel.Children.Add(closeBtn);
        
        dialog.Content = panel;
        dialog.ShowDialog(this);
    }
}