using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DrivingSchoolApp.Models;
using DrivingSchoolApp.Services;
using DrivingSchoolApp.View;
using Microsoft.Extensions.DependencyInjection;

namespace DrivingSchoolApp.ViewModels
{
    public class StudentListViewModel : ViewModelBase
    {
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public StudentListViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            Students = new ObservableCollection<Student>();
            ProfileCommand = new Command(OnProfile);
            SelectedStudent = null;
            LoadStudents();


        }

        private ObservableCollection<Student> students;
        public ObservableCollection<Student> Students
        {
            get => students;
            set
            {
                students = value;
                OnPropertyChanged("Students");
            }
        }
        private Student selectedStudent;
        public Student SelectedStudent
        {
            get => selectedStudent;
            set
            {
                
                selectedStudent = value;
                OnPropertyChanged("SelectedStudent");

                if (value != null)
                {
                    OpenProfilePage();
                }
            }
        }

        //This method open the profile page and pass into the page the selected student object
        private async void OpenProfilePage()
        {
            var navParam = new Dictionary<string, object>
                {
                    { "selectedStudent", SelectedStudent}
                };
            //Navigate to the task details page
            await Shell.Current.GoToAsync(nameof(StudentProfileView), navParam);

            //SelectedStudent  = null;
        }
        // פעולה שמחזירה לי רשימת תלמידים ושומרת אותם
        private async void LoadStudents()
        {
            List<Student> studentList = await proxy.GetAllStudents();
            if (studentList != null)
            {
                Students = new ObservableCollection<Student>(studentList);
            }
        }
        public ICommand ProfileCommand { get; }
        private void OnProfile()
        {       
            // Navigate to the profile View page
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<StudentProfileView>());
        }
       

    }
}
