
using CommunityToolkit.Mvvm.ComponentModel;

namespace MainApp.ViewModels;

public partial class ProductViewModel : ObservableObject
{
    public string Title { get; set; } = "Products";
}
