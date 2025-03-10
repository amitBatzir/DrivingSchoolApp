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
            Routing.RegisterRoute(nameof(ScheduleView), typeof(ScheduleView));
            Routing.RegisterRoute(nameof(LessonsHistoryView), typeof(LessonsHistoryView));
            Routing.RegisterRoute(nameof(StudentListView), typeof(StudentListView));
            Routing.RegisterRoute(nameof(ApprovingStudentsView), typeof(ApprovingStudentsView));
            Routing.RegisterRoute(nameof(CommentsView), typeof(CommentsView));
            Routing.RegisterRoute(nameof(TeachersListView), typeof(TeachersListView));
            Routing.RegisterRoute(nameof(ApprovingTeachersView), typeof(ApprovingTeachersView));
            Routing.RegisterRoute(nameof(ApprovingManagersView), typeof(ApprovingManagersView));
            Routing.RegisterRoute(nameof(SchoolsListView), typeof(SchoolsListView));
            Routing.RegisterRoute(nameof(CommentsView), typeof(CommentsView));
            Routing.RegisterRoute(nameof(StudentProfileView), typeof(StudentProfileView));

        }
    }
}







   

