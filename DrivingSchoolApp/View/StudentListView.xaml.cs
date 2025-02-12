using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;


public partial class StudentListView : ContentPage
{
	public StudentListView(StudentListViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}