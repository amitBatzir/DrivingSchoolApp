using System.Collections.Generic;
using System;

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
        public AppUser? LoggedInUser { get; set; }
        public List<UrgencyLevel> UrgencyLevels { get; set; } = new List<UrgencyLevel>();
        private TasksManagementWebAPIProxy proxy;
        public App(IServiceProvider serviceProvider, TasksManagementWebAPIProxy proxy)
        {
            this.proxy = proxy;
            InitializeComponent();
            LoggedInUser = null;
            
            //Start with the Login View
            MainPage = new NavigationPage(serviceProvider.GetService<LoginView>());
        }

    }
}
