//namespace DrivingSchoolApp.View;

//public partial class AddNewLessonView : ContentPage
//{
//	public AddNewLessonView()
//	{
//		InitializeComponent();
//	}
//}

using DrivingSchoolApp.Services;
using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class AddNewLessonView : ContentPage
{
    public AddNewLessonView(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider, AddNewLessonViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
        this.BindingContext = new AddNewLessonViewModel(proxy, serviceProvider);
    }
}
