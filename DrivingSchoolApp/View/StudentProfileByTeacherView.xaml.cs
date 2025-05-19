using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class StudentProfileByTeacherView : ContentPage
{
	public StudentProfileByTeacherView(StudentProfileByTeacherViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}