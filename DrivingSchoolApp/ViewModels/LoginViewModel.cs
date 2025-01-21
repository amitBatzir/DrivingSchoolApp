using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DrivingSchoolApp.Services;
using DrivingSchoolApp.Models;
using Microsoft.Win32;
using DrivingSchoolApp.ViewModels;
using DrivingSchoolApp.View;

namespace DrivingSchoolApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public LoginViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.proxy = proxy;
            LoginCommand = new Command(OnLogin);
            RegisterCommand = new Command(OnRegister);
            email = "";
            password = "";
            InServerCall = false;
            errorMsg = "";
        }

        private string email;
        private string password;

        public string Email
        {
            get => email;
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private string errorMsg;
        public string ErrorMsg
        {
            get => errorMsg;
            set
            {
                if (errorMsg != value)
                {
                    errorMsg = value;
                    OnPropertyChanged(nameof(ErrorMsg));
                }
            }
        }
        private UserTypes userType;
        public UserTypes UserType
        {
            get => userType;
            set
            {
                if (userType != value)
                {
                    userType = value;
                    OnPropertyChanged(nameof(userType));
                }
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }



        private async void OnLogin()
        {
            //Choose the way you want to blobk the page while indicating a server call
            InServerCall = true;
            ErrorMsg = "";
            //Call the server to login
            LoginInfo loginInfo = new LoginInfo
            { 
                Email = this.Email, 
                Password = this.Password,
                UserTypes = this.UserType
            };
            if(UserType == UserTypes.STUDENT)
            {
                object? o = await this.proxy.LoginAsync(loginInfo);
                InServerCall = false;
                Student s = (Student)o;
                //Set the application logged in user to be whatever user returned (null or real user)
                ((App)Application.Current).LoggedInStudent = s;
                if (s == null)
                {
                    ErrorMsg = "מייל או סיסמה לא תקינים";
                }
                else
                {
                    ErrorMsg = "";
                    //Navigate to the main page
                    AppShell shell = serviceProvider.GetService<AppShell>();
                    ((App)Application.Current).MainPage = shell;
                    Shell.Current.FlyoutIsPresented = false; //close the flyout
                    
                }
            }
            if (UserType == UserTypes.TEACHER)
            {
                object? o = await this.proxy.LoginAsync(loginInfo);
                InServerCall = false;
                Teacher t= (Teacher)o;
                //Set the application logged in user to be whatever user returned (null or real user)
                ((App)Application.Current).LoggedInTeacher = t;
                if (t == null)
                {
                    ErrorMsg = "מייל או סיסמה לא תקינים";
                }
                else
                {
                    ErrorMsg = "";
                    //Navigate to the main page
                    AppShell shell = serviceProvider.GetService<AppShell>();
                    ((App)Application.Current).MainPage = shell;
                    Shell.Current.FlyoutIsPresented = false; //close the flyout
                    
                }
            }
            if (UserType == UserTypes.MANAGER)
            {
                object? o = await this.proxy.LoginAsync(loginInfo);
                InServerCall = false;
                Manager m = (Manager)o;
                //Set the application logged in user to be whatever user returned (null or real user)
                ((App)Application.Current).LoggedInManager = m;
                if (m == null)
                {
                    ErrorMsg = "מייל או סיסמה לא תקינים";
                }
                else
                {
                    ErrorMsg = "";
                    //Navigate to the main page
                    AppShell shell = serviceProvider.GetService<AppShell>();
                    //HomePageViewModel homePageViewModel = serviceProvider.GetService<HomePageViewModel>();
                    //homePageViewModel.Refresh(); //Refresh data and user in the homepageViewModel as it is a singleton
                    ((App)Application.Current).MainPage = shell;
                    Shell.Current.FlyoutIsPresented = false; //close the flyout
                    //Shell.Current.GoToAsync("Home"); //Navigate to the Home Page tab pa
               
        
                    await Shell.Current.GoToAsync("HomePageView");
        
    }
            }  
        }

        private void OnRegister()
        {
            ErrorMsg = "";
            Email = "";
            Password = "";
            // Navigate to the Register View page
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<ChooseRegisterView>());
        }


    }
}
