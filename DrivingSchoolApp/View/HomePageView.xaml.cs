using DrivingSchoolApp.ViewModels;
namespace DrivingSchoolApp.View;

public partial class HomePageView : ContentPage
{
	public HomePageView(HomePageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}