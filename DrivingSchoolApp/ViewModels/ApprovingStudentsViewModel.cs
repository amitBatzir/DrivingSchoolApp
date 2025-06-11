using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrivingSchoolApp.View;
using DrivingSchoolApp.Services;
using DrivingSchoolApp.Models;



namespace DrivingSchoolApp.ViewModels
{
    public class ApprovingStudentsViewModel:ViewModelBase
    {
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public ApprovingStudentsViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            PendingStudents = new ObservableCollection<Student>();
            LoadPendingStudents();
            ApproveCommand = new Command<Student>(OnApproving);
            DeclineCommand = new Command<Student>(OnDeclining);
            //Check = false;
        }
        private ObservableCollection<Student> pendingStudents;
        public ObservableCollection<Student> PendingStudents
        {
            get => pendingStudents;
            set
            {
                pendingStudents = value;
                OnPropertyChanged("PendingStudents");
            }
        }

        // פעולה שמחזירה לי רשימת תלמידים ושומרת אותם
        private async void LoadPendingStudents()
        {
            List<Student> StudentList = await proxy.ShowPendingStudent(((App)Application.Current).LoggedInTeacher.UserTeacherId);
            if (StudentList != null)
            {
                PendingStudents = new ObservableCollection<Student>(StudentList);
            }
        }
       
        public Command ApproveCommand { get; }
        public async void OnApproving(Student s)
        {
            bool isWorking = await proxy.ApprovingStudent(s.UserStudentId);
            if (isWorking == true)
            {
                ((AppShell)Shell.Current).Refresh(typeof(StudentListViewModel));
                await Application.Current.MainPage.DisplayAlert("בוצע בהצלחה", $"התלמיד אושר בהצלחה", "ok");
                PendingStudents.Remove(s);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("שגיאה", $"קרתה שגיאה במהלך האישור", "ok");

            }
        }

        public Command DeclineCommand { get; }
        public async void OnDeclining(Student s)
        {
            bool isWorking = await proxy.DecliningStudent(s.UserStudentId);
            if (isWorking == true)
            {
                await Application.Current.MainPage.DisplayAlert("בוצע בהצלחה", $"התלמיד נדחה בהצלחה", "ok"); 
                PendingStudents.Remove(s);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("שגיאה", $"קרתה שגיאה במהלך הדחיה", "ok");

            }
        }

    }

}


