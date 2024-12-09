using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class RegisterManagerView : ContentPage
{
	public RegisterManagerView(RegisterManagerViewModel vm)
    {
        this.BindingContext = vm;
		InitializeComponent();
	}
}