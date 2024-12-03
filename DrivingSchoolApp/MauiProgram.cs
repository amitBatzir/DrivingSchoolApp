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
            return builder;
        }
    }
    
}
