
using CommunityToolkit.Mvvm.ComponentModel;

namespace MainApp.ViewModels;

public partial class ProductViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "Products";
}
