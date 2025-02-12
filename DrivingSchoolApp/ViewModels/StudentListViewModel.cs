using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            StudentsName = new ObservableCollection<string>();
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
        private ObservableCollection<string> studentsName;
        public ObservableCollection<string> StudentsName
        {
            get => studentsName;
            set
            {
                studentsName = value;
                OnPropertyChanged("StudentsName");
            }
        }

        // פעולה שמחזירה לי רשימת תלמידים ושומרת אותם
        private async void LoadStudents()
        {
            List<Student> studentList = await proxy.GetAllStudents();
            if (studentList != null)
            {
                foreach (Student s in studentList)
                {
                    Students.Add(s);
                    StudentsName.Add(s.FirstName + " " + s.LastName);
                }
            }
        }
    }
}
