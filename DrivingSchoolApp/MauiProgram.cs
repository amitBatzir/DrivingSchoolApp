using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using DrivingSchoolApp.Services;
using DrivingSchoolApp.View;
using DrivingSchoolApp.ViewModels;

namespace DrivingSchoolApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
            .RegisterDataServices()
            .RegisterPages()
            .RegisterViewModels();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
     
        }
        public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<HomePageView>();
            builder.Services.AddTransient<RegisterStudentView>();
            builder.Services.AddTransient<ScheduleView>();
            builder.Services.AddTransient<ChooseRegisterView>();
            builder.Services.AddTransient<RegisterManagerView>();
            builder.Services.AddTransient<RegisterTeacherView>();
            builder.Services.AddTransient<LessonsHistoryView>();
            builder.Services.AddTransient<ProfileView>();
            builder.Services.AddTransient<StudentListView>();
            builder.Services.AddTransient<TeachersListView>();
            builder.Services.AddTransient<ApprovingStudentsView>();
            builder.Services.AddTransient<ApprovingTeachersView>();
            builder.Services.AddTransient<SchoolsListView>();
            builder.Services.AddTransient<ApprovingManagersView>();
            builder.Services.AddTransient<CommentsView>();
            builder.Services.AddTransient<AppShell>(); 


            return builder;
        }

        public static MauiAppBuilder RegisterDataServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<DrivingSchoolAppWebAPIProxy>();
            return builder;
        }
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<HomePageViewModel>();
            builder.Services.AddTransient<RegisterStudentViewModel>();
            builder.Services.AddTransient<ScheduleViewModel>();
            builder.Services.AddTransient<ChooseRegisterViewModel>();
            builder.Services.AddTransient<RegisterManagerViewModel>();
            builder.Services.AddTransient<RegisterTeacherViewModel>();
            builder.Services.AddTransient<LessonsHistoryViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<StudentListViewModel>();
            builder.Services.AddTransient<TeachersListViewModel>();
            builder.Services.AddTransient<ApprovingStudentsViewModel>();
            builder.Services.AddTransient<ApprovingTeachersViewModel>();
            builder.Services.AddTransient<SchoolsListViewModel>();
            builder.Services.AddTransient<ApprovingManagersViewModel>();
            builder.Services.AddTransient<CommentsViewModel>();
            builder.Services.AddTransient<AppShellViewModel>();



            return builder;
        }
    }
    
}
