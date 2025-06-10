using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class ApprovingManagersView : ContentPage
{
	public ApprovingManagersView(ApprovingManagersViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}