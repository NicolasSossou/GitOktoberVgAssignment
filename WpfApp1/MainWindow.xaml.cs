using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using MainApp.MainViewModels;
using MainApp.Models;
using Microsoft.Extensions.DependencyInjection;
namespace MainApp;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
       DataContext= viewModel;
    }


    private void AddProduct_Click(object sender, RoutedEventArgs e) // själva objektet som triggat eventet//
    {
    }
}