using DrivingSchoolApp.Models;
using DrivingSchoolApp.Services;
using Plugin.Maui.Calendar.Models;

//using Plugin.Maui.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolApp.ViewModels
{
    public class ScheduleViewModel:ViewModelBase
    {
        private List<Lesson> lessons;
        private async void ReadLessons()
        {
            EventCollection events = new EventCollection();
            lessons = await proxy.getTeacherLessons();

            if (lessons !=  null && lessons.Count > 0)
            {
                lessons = lessons.OrderBy(l => l.DateOfLesson).ToList();
                DateTime? currentDay = null;
                List<Lesson> current = new List<Lesson>();
                foreach (Lesson lesson in lessons)
                {
                    if (currentDay == null || currentDay.Value.Date != lesson.DateOfLesson.Date)
                    {
                        currentDay = lesson.DateOfLesson.Date;
                        current = new List<Lesson>();
                        events.Add(lesson.DateOfLesson, current);
                    }
                    current.Add(lesson);
                }
            }

            Events = events;
            

        }
        private DrivingSchoolAppWebAPIProxy proxy;

        private EventCollection events;
        public EventCollection Events
        {
            get
            {
                return events;
            }
            set
            {
                this.events = value;
                OnPropertyChanged();
            }
        }
        public ScheduleViewModel(DrivingSchoolAppWebAPIProxy proxy)
        {
            this.proxy = proxy;
            Events = new EventCollection();
            ReadLessons();
        }
    }
}
