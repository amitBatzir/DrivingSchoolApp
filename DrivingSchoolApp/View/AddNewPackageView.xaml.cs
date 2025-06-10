using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class AddNewPackageView : ContentPage
{
	public AddNewPackageView(AddNewPackageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}