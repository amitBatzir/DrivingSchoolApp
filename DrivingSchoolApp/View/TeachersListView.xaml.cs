using DrivingSchoolApp.ViewModels;
namespace DrivingSchoolApp.View;


public partial class TeachersListView : ContentPage
{
	public TeachersListView(TeachersListViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}