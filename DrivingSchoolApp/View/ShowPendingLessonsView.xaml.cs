using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class ShowPendingLessonsView : ContentPage
{
	public ShowPendingLessonsView(ShowPendingLessonsViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}