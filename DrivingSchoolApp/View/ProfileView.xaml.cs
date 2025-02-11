using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class ProfileView : ContentPage
{
	public ProfileView(ProfileViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}