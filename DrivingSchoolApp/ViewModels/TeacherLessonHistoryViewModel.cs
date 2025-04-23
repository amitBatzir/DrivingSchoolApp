using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrivingSchoolApp.Services;
using DrivingSchoolApp.Models;

namespace DrivingSchoolApp.ViewModels
{
    public class TeacherLessonHistoryViewModel:ViewModelBase
    {
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        
      

        public TeacherLessonHistoryViewModel(DrivingSchoolAppWebAPIProxy proxy)
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
            Teacher t = ((App)Application.Current).LoggedInTeacher;
            List<Lesson> PreviousLessonsList = await proxy.getTeacherPreviousLessons(t.UserTeacherId);
            if (PreviousLessonsList != null)
            {
                Lessons = new ObservableCollection<Lesson>();
                foreach (Lesson l in PreviousLessonsList)
                {
                    if (l.DidExist == true)
                    {
                        WasDone = "התקיים";
                    }
                    else
                    {
                        WasDone = "בוטל";
                    }
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
