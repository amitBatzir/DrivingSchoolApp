using System.Runtime.CompilerServices;
using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class LessonsHistoryView : ContentPage
{
	public LessonsHistoryView(LessonsHistoryViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}