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
    public class FutureLessonsViewModel:ViewModelBase
    {
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        public FutureLessonsViewModel(DrivingSchoolAppWebAPIProxy proxy)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            Lessons = new ObservableCollection<Lesson>();
            LoadFutureLessons();
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
        private async void LoadFutureLessons()
        {
            List<Lesson> FutureLessonsList = await proxy.GetFutureLessons();
            if (FutureLessonsList != null)
            {
                Lessons = new ObservableCollection<Lesson>(FutureLessonsList);
            }
        }
    }
}
