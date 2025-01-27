using DrivingSchoolApp.View;
using DrivingSchoolApp.ViewModels;
namespace DrivingSchoolApp
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
            RegisterRoutes();
        }


        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(HomePageView), typeof(HomePageView));
            Routing.RegisterRoute(nameof(ProfileView), typeof(ProfileView));
        }
    }
}
