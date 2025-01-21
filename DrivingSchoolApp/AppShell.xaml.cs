using DrivingSchoolApp.View;
using DrivingSchoolApp.ViewModels;
namespace DrivingSchoolApp
{
    public partial class AppShell : Shell
    {
        public AppShell(/*ShellViewModel vm*/)
        {
            //this.BindingContext = vm;
            InitializeComponent();
            RegisterRoutes();
        }


        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(HomePageView), typeof(HomePageView));

        }
    }
}
