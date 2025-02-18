using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class StudentProfileView : ContentPage
{
	public StudentProfileView(StudentProfileViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}