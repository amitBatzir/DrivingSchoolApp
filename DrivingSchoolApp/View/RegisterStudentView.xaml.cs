using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class RegisterStudentView : ContentPage
{
	public RegisterStudentView(RegisterStudentViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}