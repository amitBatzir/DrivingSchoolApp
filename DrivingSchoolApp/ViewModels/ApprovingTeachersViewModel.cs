using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrivingSchoolApp.Services;
using DrivingSchoolApp.Models;
using System.Windows.Input;

namespace DrivingSchoolApp.ViewModels
{
    public class ApprovingTeachersViewModel:ViewModelBase
    {
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public ApprovingTeachersViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            PendingTeachers = new ObservableCollection<Teacher>();
            LoadPendingTeachers();
           ApproveCommand = new Command<Teacher>(OnApproving);
            DeclineCommand = new Command<Teacher>(OnDeclining);
                //Check = false;
        }
        private ObservableCollection<Teacher> pendingTeachers;
        public ObservableCollection<Teacher> PendingTeachers
        {
            get => pendingTeachers;
            set
            {
                pendingTeachers = value;
                OnPropertyChanged("PendingTeachers");
            }
        }

        // פעולה שמחזירה לי רשימת מורים ושומרת אותם
        private async void LoadPendingTeachers()
        {
            List<Teacher> TeacherList = await proxy.ShowPendingTeachers(((App)Application.Current).LoggedInManager.UserManagerId);
            if (TeacherList != null)
            {
                PendingTeachers = new ObservableCollection<Teacher>(TeacherList);
            }
        }
        //private bool check;
        //public bool Check
        //{
        //    get
        //    {
        //        return SelectedTeacher != null;
        //    }
        //    set
        //    {
        //        check = value;
        //    }
        //}
        //private Teacher selectedTeacher;
        //public Teacher SelectedTeacher
        //{
        //    get
        //    {
        //        return this.selectedTeacher;
        //    }
        //    set
        //    {
        //        selectedTeacher = value;
        //        OnPropertyChanged("SelectedTeacher");
        //    }
        //}
        public Command ApproveCommand { get; }
        public async void OnApproving(Teacher t)
        {
          bool isWorking = await proxy.ApprovingTeacher(t.UserTeacherId);
            if (isWorking == true)
            {
                ((AppShell)Shell.Current).Refresh(typeof(TeachersListViewModel));
                await Application.Current.MainPage.DisplayAlert("בוצע בהצלחה", $"המורה אושר בהצלחה", "ok");
                PendingTeachers.Remove(t);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("שגיאה", $"קרתה שגיאה במהלך האישור", "ok");

            }
        }
       
        public Command DeclineCommand { get; }    
        public async void OnDeclining(Teacher t)
        {
            bool isWorking = await proxy.DecliningTeacher(t.UserTeacherId);
            if (isWorking == true)
            {
                await Application.Current.MainPage.DisplayAlert("בוצע בהצלחה", $"המורה נדחה בהצלחה", "ok"); 
                PendingTeachers.Remove(t);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("שגיאה", $"קרתה שגיאה במהלך הדחיה", "ok");

            }
        }
    }
}
