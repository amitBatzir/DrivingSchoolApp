using DrivingSchoolApp.Models;
using DrivingSchoolApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DrivingSchoolApp.ViewModels
{
    public class AppShellViewModel : ViewModelBase
    {
        private Student? currentStudent;     

        private Teacher? currentTeacher;

        private Manager? currentManager;

        private IServiceProvider serviceProvider;

        public AppShellViewModel(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            Check();
            LogoutCommand = new Command(OnLogOut);
        }

        public void Check()
        {
            this.currentStudent = ((App)Application.Current).LoggedInStudent;
            this.currentTeacher = ((App)Application.Current).LoggedInTeacher;
            this.currentManager = ((App)Application.Current).LoggedInManager;
        }

        public bool IsStudent
        {
            get
            {              
                return currentStudent != null;
            }
        }
        public bool IsTeacher
        {
            get
            {              
                return currentTeacher != null;
            }
        }
        public bool IsManager
        {
            get
            {
                return currentManager != null;
            }
        }
        public bool NotManager
        {
            get
            {
                return currentManager == null;
            }
        }
        public bool NotStudent
        {
            get
            {
                return currentStudent == null;
            }
        }
        public bool AppManager
        {
            get
            {
               return (currentStudent == null && currentTeacher == null && currentManager == null);
            }
        }
       

       
        public ICommand LogoutCommand { get; set; }
        //this method will be trigger upon Logout button click
        public async void OnLogOut()
        {
            bool result = await Application.Current.MainPage.DisplayAlert("התנתקות", $"אתה בטוח שאתה רוצה להתנתק?", "אישור", "ביטול");//if the check returned not null means that the user exist, shows a message
            if (result)
            {
                ((App)Application.Current).LoggedInManager = null;
                Application.Current.MainPage = new NavigationPage(serviceProvider.GetService<LoginView>());

            }
        }
    }
}

