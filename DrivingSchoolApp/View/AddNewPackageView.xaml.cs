namespace DrivingSchoolApp.View;

public partial class AddNewPackageView : ContentPage
{
	public AddNewPackageView(AddNewPackageView vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}