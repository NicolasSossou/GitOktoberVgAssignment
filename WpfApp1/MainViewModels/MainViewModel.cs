using System.Collections.ObjectModel;
using MainApp.Models;

public class MainViewModel
{
    public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();

    public void Add_Product_Click(string name, string category, string manufacturer, string price)
    {
        Products.Add(new Product
        {
            Name = name,
            Category = category,
            Manufacturer = manufacturer,
            Price = price
        });
    }
}
