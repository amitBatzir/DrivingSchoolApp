using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrivingSchoolApp.Models;
using DrivingSchoolApp.Services;
using DrivingSchoolApp;


namespace DrivingSchoolApp.ViewModels
{
    public class TeachersListViewModel:ViewModelBase
    {
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public TeachersListViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            //Students = new ObservableCollection<Student>();
            TeachersName = new ObservableCollection<string>();
            LoadTeachers();
        }

       
        private ObservableCollection<string> teachersName;
        public ObservableCollection<string> TeachersName
        {
            get => teachersName;
            set
            {
                teachersName = value;
                OnPropertyChanged("TeachersName");
            }
        }

        // פעולה שמחזירה לי רשימת מורים ושומרת אותם
        private async void LoadTeachers()
        {
            List<Teacher> TeacherList = await proxy.GetTeacherOfSchool(((App)Application.Current).LoggedInManager.UserManagerId);
            if (TeacherList != null)
            {
                foreach (Teacher t in TeacherList)
                {
                    TeachersName.Add(t.FirstName + " " + t.LastName);
                }
            }
        }
    }
}
