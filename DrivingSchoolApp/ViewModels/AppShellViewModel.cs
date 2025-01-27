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
      
        public AppShellViewModel(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            if (UserType == UserTypes.STUDENT)
            {
                this.currentStudent = ((App)Application.Current).LoggedInStudent;
            }
            else if (UserType == UserTypes.TEACHER)
            {
                this.currentTeacher = ((App)Application.Current).LoggedInTeacher;
            }
            else
            {
                this.currentManager = ((App)Application.Current).LoggedInManager;
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

            if (UserType == UserTypes.STUDENT)
            {
                ((App)Application.Current).LoggedInStudent = null;
            }
            else if (UserType == UserTypes.TEACHER)
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
