using DrivingSchoolApp.ViewModels;
namespace DrivingSchoolApp.View;

public partial class FutureLessonsView : ContentPage
{
	public FutureLessonsView(FutureLessonsViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}