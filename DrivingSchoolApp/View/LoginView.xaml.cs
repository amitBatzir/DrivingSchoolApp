using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class LoginView : ContentPage
{
	public LoginView(LoginViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
    //private void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    //{
    //    if (e.Value)
    //    {
    //        var radioButton = sender as RadioButton;
    //        DisplayAlert("Selection", $"You selected: {radioButton.Content}", "OK");
    //    }
    //}
}