using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class RegisterView : ContentPage
{
	public RegisterView(RegisterStudentViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}