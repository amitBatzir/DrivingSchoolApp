﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DrivingSchoolApp.Models;
using DrivingSchoolApp.Services;
using DrivingSchoolApp.View;

namespace DrivingSchoolApp.ViewModels
{
    public class StudentListViewModel : ViewModelBase
    {
        private Manager currentManager;
        private Teacher currentTeacher;
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public StudentListViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            Students = new ObservableCollection<Student>();
            //ProfileCommand = new Command(OnProfile);
            SelectedStudent = null;
            
            currentTeacher = ((App)Application.Current).LoggedInTeacher;
            currentManager = ((App)Application.Current).LoggedInManager;
            LoadStudents();
            GoToProfileCommand = new Command<Student>(OnGoToProfile);

        }
        public ICommand GoToProfileCommand { get; }
        public async void OnGoToProfile(Student s)
        {
            if (s != null)
            {
                var navParam = new Dictionary<string, object>
                {
                    {"selectedStudent",s}
                };
                await Shell.Current.GoToAsync("StudentProfileByTeacherView", navParam);
                SelectedStudent = null;
            }
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
                    OnGoToProfile(selectedStudent);
                }
            }
        }
        //private async void OnSingleSelection(Student s)
        //{
        //    if (s != null)
        //    {
        //        var navParam = new Dictionary<string, object>
        //        {
        //            {"selectedTeacher",t}
        //        };
        //        await Shell.Current.GoToAsync("TeacherProfileView", navParam);
        //        SelectedTeacher = null;
        //    }
        //}
        //This method open the profile page and pass into the page the selected student object

        // פעולה שמחזירה לי רשימת תלמידים ושומרת אותם
        private async void LoadStudents()
        {
            List<Student> StudentList =  await proxy.GetAllStudentsOfSchool();
            if (StudentList != null)
            {
                Students = new ObservableCollection<Student>(StudentList);
            }
        }

        public override void Refresh()
        {
            base.Refresh();

            LoadStudents();
        }

    }
}
