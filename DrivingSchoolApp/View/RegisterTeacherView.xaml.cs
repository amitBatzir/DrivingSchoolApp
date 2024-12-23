using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp.View;

public partial class RegisterTeacherView : ContentPage
{
	public RegisterTeacherView(RegisterTeacherViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
    }
    //private void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    //{
    //    if (e.Value)
    //    {
    //        var radioButton = sender as RadioButton;
    //        RegisterTeacherViewModel vm = (RegisterTeacherViewModel)this.BindingContext;
    //        if (radioButton.Value.ToString() == "0")
    //            vm.UserType = Models.UserTypes.STUDENT;
    //        else if (radioButton.Value.ToString() == "1")
    //            vm.UserType = Models.UserTypes.TEACHER;
    //        else if (radioButton.Value.ToString() == "2")
    //            vm.UserType = Models.UserTypes.MANAGER;
    //        //DisplayAlert("סוג משתמש ", $"אתה בחרת {radioButton.Content}", "אישור");
    //    }
    //}
}