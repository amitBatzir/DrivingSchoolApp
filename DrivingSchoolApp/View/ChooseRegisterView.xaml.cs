using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class ChooseRegisterView : ContentPage
{
	public ChooseRegisterView(ChooseRegisterViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
	}
}