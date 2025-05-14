using DrivingSchoolApp.ViewModels;
namespace DrivingSchoolApp.View;

public partial class PackagesView : ContentPage
{
	public PackagesView(PackagesViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}