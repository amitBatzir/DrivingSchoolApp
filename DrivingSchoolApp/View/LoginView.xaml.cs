using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class LoginView : ContentPage
{
	public LoginView(LoginViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
    private void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            var radioButton = sender as RadioButton;
            LoginViewModel vm = (LoginViewModel) this.BindingContext;
            if (radioButton.Value.ToString() == "0")
                vm.UserType = Models.UserTypes.STUDENT;
            else if (radioButton.Value.ToString() == "1")
                vm.UserType = Models.UserTypes.TEACHER;
            else if (radioButton.Value.ToString() == "2")
                vm.UserType = Models.UserTypes.MANAGER;
            //DisplayAlert("סוג משתמש ", $"אתה בחרת {radioButton.Content}", "אישור");
        }
    }
}