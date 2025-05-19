using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrivingSchoolApp.Models;
using DrivingSchoolApp.Services;
using DrivingSchoolApp;
using System.Windows.Input;
using DrivingSchoolApp.View;


namespace DrivingSchoolApp.ViewModels
{
    public class TeachersListViewModel : ViewModelBase
    {
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public TeachersListViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            Teachers = new ObservableCollection<Teacher>();
            LoadTeachers();
            GoToProfileCommand = new Command<Teacher>(OnGoToProfile);
        }
        public ICommand GoToProfileCommand { get; }
        public async void OnGoToProfile(Teacher t)
        {
            if (t != null)
            {
                var navParam = new Dictionary<string, object>
                {
                    {"selectedTeacher",t}
                };
                await Shell.Current.GoToAsync("TeacherProfileByManagerView", navParam);
                SelectedTeacher = null;
            }
        }

        private ObservableCollection<Teacher> teachers;
        public ObservableCollection<Teacher> Teachers
        {
            get => teachers;
            set
            {
                teachers = value;
                OnPropertyChanged("Teachers");
            }
        }

        private Teacher selectedTeacher;
        public Teacher SelectedTeacher
        {
            get
            {
                return selectedTeacher;
            }
            set
            {
                selectedTeacher = value;
                OnGoToProfile(selectedTeacher);
                OnPropertyChanged();
            }
        }
        //private async void OnSingleSelection(Teacher t)
        //{
        //    if (t != null)
        //    {
        //        var navParam = new Dictionary<string, object>
        //        {
        //            {"selectedTeacher",t}
        //        };
        //        await Shell.Current.GoToAsync("TeacherProfileView", navParam);
        //        SelectedTeacher = null;
        //    }
        //}
        // פעולה שמחזירה לי רשימת מורים ושומרת אותם
        private async void LoadTeachers()
        {
            List<Teacher> TeacherList = await proxy.GetTeacherOfSchool(((App)Application.Current).LoggedInManager.UserManagerId);
            if (TeacherList != null)
            {
                Teachers = new ObservableCollection<Teacher>(TeacherList);
            }
        }

    }
}

