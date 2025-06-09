using AndroidX.Annotations;
using DrivingSchoolApp.Models;
using DrivingSchoolApp.Services;
using Plugin.Maui.Calendar.Models;

//using Plugin.Maui.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DrivingSchoolApp.ViewModels
{
    public class ScheduleViewModel:ViewModelBase
    {
        public ScheduleViewModel(DrivingSchoolAppWebAPIProxy proxy)
        {
            this.proxy = proxy;
            Events = new EventCollection();
            ReadLessons();
            ShowPendingLessonCommand = new Command(OnShowPendingLessons);
            ApproveCommand = new Command<Lesson>(OnApproving);
            DeclineCommand = new Command<Lesson>(OnDeclining);
            ShowLessonDetailsCommand = new Command<Lesson>(OnShowLessonDetails);


        }
        public Command ShowLessonDetailsCommand { get; }
         private async void OnShowLessonDetails(Lesson lesson)
        {
            if (lesson == null) return;

            string message = $"שיעור עם {lesson.Student?.FullName ?? "תלמיד לא ידוע"}\n" +
                             $"תאריך: {lesson.DateOfLesson}\n" +
                             $"מיקום איסוף: {lesson.PickUpLoc}\n" +
                             $"מיקום הורדה: {lesson.DropOffLoc}";

            await Application.Current.MainPage.DisplayAlert("פרטי שיעור", message, "סגור");
        }

        public Command ShowPendingLessonCommand { get; set; }
        private async void OnShowPendingLessons()
        {
            await Shell.Current.GoToAsync("ShowPendingLessonsView");
        }

        private List<Lesson> lessons;
        private async void ReadLessons()
        {
            InServerCall = true;
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
            InServerCall = false;
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
        public Command ApproveCommand { get; }
        public async void OnApproving(object obj)
        {
            if (obj is Lesson)
            {
                Lesson l = (Lesson)obj;
                bool isWorking = await proxy.Approvinglessons(l.LessonId);
                if (isWorking == true)
                {
                    await Application.Current.MainPage.DisplayAlert("בוצע בהצלחה", $"השיעור נקבע בהצלחה", "ok");
                    ReadLessons();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("שגיאה", $"קרתה שגיאה במהלך קביעת השיעור", "ok");

                }
            }
            
        }

        public Command DeclineCommand { get; }
        public async void OnDeclining(object obj) // להבין עם עופר איך אני מורידה מרשימת השיעורים שמחכים לאישור אחרי
        {
            if (obj is Lesson)
            {
                Lesson l = (Lesson)obj;
                bool isWorking = await proxy.DecliningLessons(l.LessonId);
                if (isWorking == true)
                {
                    await Application.Current.MainPage.DisplayAlert("בוצע בהצלחה", $"השיעור נדחה בהצלחה", "ok");
                    ReadLessons();
                    
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("שגיאה", $"קרתה שגיאה במהלך הדחיה", "ok");

                }
            }
            
        }

        public override void Refresh()
        {
            base.Refresh();

            ReadLessons();
        }

    }
}
