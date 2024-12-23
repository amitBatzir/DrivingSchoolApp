using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class RegisterTeacherView : ContentPage
{
	public RegisterTeacherView(RegisterTeacherViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
    }
}