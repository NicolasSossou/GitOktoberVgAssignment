using Infrastructure.Models;
using MainApp.MainViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
namespace MainApp;

public partial class MainWindow : Window
{
    List<Product> _productList = new List<Product>();
    public MainWindow()
    {
        InitializeComponent();
     
    }
    private void Add_Product_Click(object sender, RoutedEventArgs e)
    {
        Product product = new Product
        {
            ProductTitle = ProductNameTextBox.Text,
            ProductPrice = decimal.TryParse(ProductPriceTextBox.Text, out var price) ? price : 0,
            Category = CategoryTextBox.Text,
            Manufacturer = ManufacturerTextBox.Text,
        };

        _productList.Add(product);

        Products.Items.Clear();
        foreach (var p in _productList)
        {
            Products.Items.Add($"{product.ProductTitle} | {product.Category} | {product.Manufacturer} | {product.ProductPrice}");
        }

        ProductNameTextBox.Text = "";
        ProductPriceTextBox.Text = "";
        CategoryTextBox.Text = "";
        ManufacturerTextBox.Text = "";
    }
}
