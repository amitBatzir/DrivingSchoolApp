using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class TeacherLessonHistoryView : ContentPage
{
	public TeacherLessonHistoryView(TeacherLessonHistoryViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}