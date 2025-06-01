using DrivingSchoolApp.Models;
using DrivingSchoolApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace DrivingSchoolApp.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private Student currentStudent;
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public HomePageViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.proxy = proxy;
            this.currentStudent = ((App)Application.Current).LoggedInStudent;
            SchoolName = currentStudent.SchoolName;
        }
     
        private String schoolName;
        public String SchoolName
        {
            get => schoolName;
            set
            {
                schoolName = value;
                OnPropertyChanged("SchoolName");
            }

        }
    }
}

