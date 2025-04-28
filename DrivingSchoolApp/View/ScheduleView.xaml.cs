using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class ScheduleView : ContentPage
{
	public ScheduleView(ScheduleViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}