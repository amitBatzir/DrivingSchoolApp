using System.Collections.Generic;
using System;
using DrivingSchoolApp.Models;
using DrivingSchoolApp.Services;
using DrivingSchoolApp.View;
namespace DrivingSchoolApp
{
    public partial class App : Application
    {
        //public App()
        //{
        //    InitializeComponent();

        //    MainPage = new AppShell();
        //}
        //Application level variables
        public Student? LoggedInStudent { get; set; }
        public Teacher? LoggedInTeacher { get; set; }
        public Manager? LoggedInManager { get; set; }

        public List<LessonStatuses> LessonStatuses { get; set; }
      
        private DrivingSchoolAppWebAPIProxy proxy;
        public App(IServiceProvider serviceProvider, DrivingSchoolAppWebAPIProxy proxy)
        {
            this.proxy = proxy;
            InitializeComponent();
            LoggedInStudent = null;
            LoggedInTeacher = null;
            LoggedInManager = null;

            LessonStatuses = new List<LessonStatuses>();
            LessonStatuses.Add(new Models.LessonStatuses() { StatusId = 1, StatusDescription = "מחכה לאישור" });
            LessonStatuses.Add(new Models.LessonStatuses() { StatusId = 2, StatusDescription = "נקבע" });
            LessonStatuses.Add(new Models.LessonStatuses() { StatusId = 3, StatusDescription = "בוצע" });
            LessonStatuses.Add(new Models.LessonStatuses() { StatusId = 4, StatusDescription = "לא אושר" });
            LessonStatuses.Add(new Models.LessonStatuses() { StatusId = 5, StatusDescription = "בוטל" });
            //Start with the Login View
            MainPage = new NavigationPage(serviceProvider.GetService<LoginView>());
        }

    }
}
