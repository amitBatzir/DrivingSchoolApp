using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class ApprovingTeachersView : ContentPage
{
	public ApprovingTeachersView(ApprovingTeachersViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}