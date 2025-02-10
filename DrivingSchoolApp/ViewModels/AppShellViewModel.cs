using DrivingSchoolApp.Models;
using DrivingSchoolApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
       

        //this command will be used for logout menu item
        public Command LogoutCommand
        {
            get
            {
                return new Command(OnLogout);
            }
        }
        //this method will be trigger upon Logout button click
        public void OnLogout()
        {

            if (IsStudent)
            {
                ((App)Application.Current).LoggedInStudent = null;
            }
            else if (IsManager)
            {
                ((App)Application.Current).LoggedInTeacher = null;
            }
            else
            {
                ((App)Application.Current).LoggedInManager = null;
            }

                ((App)Application.Current).MainPage = new NavigationPage(serviceProvider.GetService<LoginView>());
        }
    }
}

