using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class TeacherProfileView : ContentPage
{
	public TeacherProfileView(TeacherProfileViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}