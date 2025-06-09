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
            Routing.RegisterRoute(nameof(TeacherProfileView), typeof(TeacherProfileView));
            Routing.RegisterRoute(nameof(ProfileView), typeof(ProfileView));
            Routing.RegisterRoute(nameof(FutureLessonsView), typeof(FutureLessonsView));
            Routing.RegisterRoute(nameof(TeacherLessonHistoryView), typeof(TeacherLessonHistoryView));
            Routing.RegisterRoute(nameof(AddNewLessonView), typeof(AddNewLessonView));
            Routing.RegisterRoute(nameof(PackagesView), typeof(PackagesView));
            Routing.RegisterRoute(nameof(TeacherProfileByManagerView), typeof(TeacherProfileByManagerView));
            Routing.RegisterRoute(nameof(StudentProfileByTeacherView), typeof(StudentProfileByTeacherView));
            Routing.RegisterRoute(nameof(ShowPendingLessonsView), typeof(ShowPendingLessonsView));
           Routing.RegisterRoute(nameof(AddNewPackageView), typeof(AddNewPackageView));

            ;

        }

        public event Action<Type> DataChanged;
        public void Refresh(Type type)
        {
            if (DataChanged != null)
            {
                DataChanged(type);
            }
        }
    }
}







   

