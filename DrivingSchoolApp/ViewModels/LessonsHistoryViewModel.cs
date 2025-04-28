using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DrivingSchoolApp.Models;
using DrivingSchoolApp.Services;
using DrivingSchoolApp.View;
using static Microsoft.Maui.ApplicationModel.Permissions;
using System.Collections.ObjectModel;

namespace DrivingSchoolApp.ViewModels
{
    public class LessonsHistoryViewModel: ViewModelBase
    {
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;


        public LessonsHistoryViewModel(DrivingSchoolAppWebAPIProxy proxy)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            Lessons = new ObservableCollection<Lesson>();
            LoadPreviousLessons();
            WasDone = "";
        }
        private ObservableCollection<Lesson> lessons;
        public ObservableCollection<Lesson> Lessons
        {
            get => lessons;
            set
            {
                lessons = value;
                OnPropertyChanged("Lessons");
            }
        }
       
        private async void LoadPreviousLessons()
        {
            Student s = ((App)Application.Current).LoggedInStudent;
            List<Lesson> PreviousLessonsList = await proxy.GetStudentPreviousLessons(s.UserStudentId);
            if (PreviousLessonsList != null)
            {
                Lessons = new ObservableCollection<Lesson>();
                foreach (Lesson l in PreviousLessonsList)
                {
                    //if (l.DidExist == true)
                    //{
                    //    WasDone = "התקיים";
                    //}
                    //else
                    //{
                    //    WasDone = "בוטל";
                    //}
                    Lessons.Add(l);
                }
                
            }
        }

        private string wasDone;
        public string WasDone
        {
            get => wasDone;
            set
            {
                wasDone = value;
                OnPropertyChanged("WasDone");
            }
        }
        

    }
}
