using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;

namespace MainApp;

public partial class MainWindow : Window
{
    private ObservableCollection<string> _products = ["Items Loaded"];
    public MainWindow()
    {
        InitializeComponent();
        Products.ItemsSource = _products;
    }


    private void AddProduct_Click(object sender, RoutedEventArgs e) // själva objektet som triggat eventet//
    {
       
        Products.Items.Add("Button Clicked");
      
    }

}