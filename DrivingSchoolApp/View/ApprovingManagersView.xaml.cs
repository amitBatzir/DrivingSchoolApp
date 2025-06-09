namespace DrivingSchoolApp.View;

public partial class ApprovingManagersView : ContentPage
{
	public ApprovingManagersView(ApprovingManagersView vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}