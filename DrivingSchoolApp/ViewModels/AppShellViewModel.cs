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
        private Student? currentStudent = null;     

        private Teacher? currentTeacher = null;

        private Manager? currentManager = null;

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
                return currentManager != null && !AppManager;
            }
        }
        public bool NotManager
        {
            get
            {
                return !IsManager && !AppManager;
            }
        }
        public bool NotStudent
        {
            get
            {
                return currentStudent == null && !AppManager;
            }
        }
        public bool AppManager
        {
            get
            {
                //Only managers with company email are syste,m admin
               return (currentManager != null && currentManager.ManagerEmail.Contains("@driverseat.com"));
            }
        }
       

       
        public ICommand LogoutCommand { get; set; }
        //this method will be trigger upon Logout button click
        public async void OnLogOut()
        {
            bool result = await Application.Current.MainPage.DisplayAlert("התנתקות", $"אתה בטוח שאתה רוצה להתנתק?", "אישור", "ביטול");//if the check returned not null means that the user exist, shows a message
            if (result)
            {
                ((App)Application.Current).LoggedInStudent = null;
                ((App)Application.Current).LoggedInTeacher = null;
                ((App)Application.Current).LoggedInManager = null;
                Application.Current.MainPage = new NavigationPage(serviceProvider.GetService<LoginView>());

            }
        }
    }
}

