using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class TeacherProfileByManagerView : ContentPage
{
	public TeacherProfileByManagerView(TeacherProfileByManagerViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}