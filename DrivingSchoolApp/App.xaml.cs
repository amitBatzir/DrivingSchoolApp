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
      
        private DrivingSchoolAppWebAPIProxy proxy;
        public App(IServiceProvider serviceProvider, DrivingSchoolAppWebAPIProxy proxy)
        {
            this.proxy = proxy;
            InitializeComponent();
            LoggedInStudent = null;
            LoggedInTeacher = null;
            LoggedInManager = null;
            //Start with the Login View
            MainPage = new NavigationPage(serviceProvider.GetService<LoginView>());
        }

    }
}
