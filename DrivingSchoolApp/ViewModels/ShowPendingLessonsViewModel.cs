using DrivingSchoolApp.Models;
using DrivingSchoolApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchoolApp.ViewModels
{
    public class ShowPendingLessonsViewModel:ViewModelBase
    {
        private DrivingSchoolAppWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public ShowPendingLessonsViewModel(DrivingSchoolAppWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            PendingLessons = new ObservableCollection<Lesson>();
            LoadPendingLessons();
            ApproveCommand = new Command<Lesson>(OnApproving);
            DeclineCommand = new Command<Lesson>(OnDeclining);
            ShowLessonDetailsCommand = new Command<Lesson>(OnShowLessonDetails);
            //Check = false;
        }
        public Command ShowLessonDetailsCommand { get; }
        private async void OnShowLessonDetails(Lesson lesson)
        {
            if (lesson == null) return;

            string message = $"מיקום איסוף: {lesson.PickUpLoc}\n" +
                             $"מיקום הורדה: {lesson.DropOffLoc}";

            await Application.Current.MainPage.DisplayAlert("פרטי שיעור", message, "סגור");
        }
        private ObservableCollection<Lesson> pendingLessons;
        public ObservableCollection<Lesson> PendingLessons
        {
            get => pendingLessons;
            set
            {
                pendingLessons = value;
                OnPropertyChanged("PendingLessons");
            }
        }

        // פעולה שמחזירה לי רשימת שיעורים ושומרת אותם
        private async void LoadPendingLessons()
        {
            List<Lesson> lessonsList = await proxy.ShowPendingLessons();
            if (lessonsList != null)
            {
                PendingLessons = new ObservableCollection<Lesson>(lessonsList);
            }
        }

        public Command ApproveCommand { get; }
        public async void OnApproving(Lesson l)
        {
            bool isWorking = await proxy.Approvinglessons(l.LessonId);
            if (isWorking == true)
            {
                await Application.Current.MainPage.DisplayAlert("בוצע בהצלחה", $"השיעור נקבע בהצלחה", "ok");
                PendingLessons.Remove(l);
                ((AppShell)Shell.Current).Refresh(typeof(ScheduleViewModel));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("שגיאה", $"קרתה שגיאה במהלך קביעת השיעור", "ok");

            }

            
        }

        public Command DeclineCommand { get; }
        public async void OnDeclining(Lesson l)
        {
            bool isWorking = await proxy.DecliningLessons(l.LessonId);
            if (isWorking == true)
            {
                await Application.Current.MainPage.DisplayAlert("בוצע בהצלחה", $"השיעור נדחה בהצלחה", "ok");
                PendingLessons.Remove(l);
                ((AppShell)Shell.Current).Refresh(typeof(ScheduleViewModel));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("שגיאה", $"קרתה שגיאה במהלך הדחיה", "ok");

            }
        }
    }
}






   