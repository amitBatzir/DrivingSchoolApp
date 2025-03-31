using DrivingSchoolApp.ViewModels;
namespace DrivingSchoolApp.View;
public partial class ApprovingStudentsView : ContentPage
{
	public ApprovingStudentsView(ApprovingStudentsViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}